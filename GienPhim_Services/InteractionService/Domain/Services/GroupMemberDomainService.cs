using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Exceptions.Permission;
using GienPhim_Services.InteractionService.Domain.Exceptions.State;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;

namespace GienPhim_Services.InteractionService.Domain.Services
{
    public class GroupMemberDomainService : IGroupMemberDomainService
    {
        public void EnsureCanJoin(ChatGroup group, GroupMember? existingMembership, DateTime now)
        {
            if (group is null)
                throw new InvalidGroupMemberException("Không thể tham gia phòng chat không tồn tại.");

            if (group.IsDeleted || now > group.AutoDeleteAt || now > group.ShowEndTime)
                throw new InvalidGroupMemberException("Phòng chat đã kết thúc hoặc bị xoá, không thể tham gia.");

            if (existingMembership is not null)
                throw new DuplicateGroupMemberException(existingMembership.UserId, existingMembership.GroupId);
        }

        public void EnsureCanLeave(ChatGroup group, GroupMember member, bool isLastOwner)
        {
            if (group is null || member is null)
                throw new InvalidGroupMemberException("Dữ liệu thành viên hoặc phòng chat không hợp lệ.");

            // Nếu là owner duy nhất thì không cho leave
            if (member.IsOwner && isLastOwner)
                throw new GroupOwnerOperationNotAllowedException(
                    "Chủ phòng cuối cùng không thể rời phòng chat. Hãy chuyển quyền chủ phòng cho người khác trước.");
        }

        public void EnsureCanTransferOwnership(ChatGroup group, GroupMember currentOwner, GroupMember newOwner)
        {
            if (group is null || currentOwner is null || newOwner is null)
                throw new InvalidGroupMemberException("Dữ liệu thành viên hoặc phòng chat không hợp lệ.");

            if (!currentOwner.IsOwner)
                throw new GroupMemberPermissionException("Chỉ chủ phòng mới được chuyển quyền chủ phòng.");

            if (currentOwner.GroupId != newOwner.GroupId || currentOwner.GroupId != group.GroupId)
                throw new InvalidGroupMemberException("Chỉ có thể chuyển quyền trong cùng một phòng chat.");
        }

        public void EnsureCanUpdateMemberPermissions(ChatGroup group, GroupMember actingMember, GroupMember targetMember)
        {
            if (group is null || actingMember is null || targetMember is null)
                throw new InvalidGroupMemberException("Dữ liệu thành viên hoặc phòng chat không hợp lệ.");

            // Chỉ owner mới được sửa quyền người khác
            if (!actingMember.IsOwner)
                throw new GroupMemberPermissionException("Chỉ chủ phòng mới được thay đổi quyền thành viên.");

            // Không cho hạ/khóa chính mình qua luồng này
            if (actingMember.GroupMemberId == targetMember.GroupMemberId)
                throw new GroupMemberPermissionException("Không thể thay đổi quyền của chính mình qua thao tác này.");
        }
    }
}
