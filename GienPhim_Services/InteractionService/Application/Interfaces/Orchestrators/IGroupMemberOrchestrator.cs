using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators
{
    public interface IGroupMemberOrchestrator
    {
        // User tham gia phòng chat
        Task<GroupMember> JoinAsync(ChatGroup group, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // User rời phòng chat
        Task LeaveAsync(ChatGroup group, GroupMember member, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // Chuyển quyền chủ phòng
        Task TransferOwnershipAsync(ChatGroup group, GroupMember currentOwner, GroupMember newOwner, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // Cập nhật quyền của thành viên (mute, canChat, canControlFilm)
        Task UpdatePermissionsAsync(ChatGroup group, GroupMember actingMember, GroupMember targetMember, GroupMemberUpdatePermissionsDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);
    }
}
