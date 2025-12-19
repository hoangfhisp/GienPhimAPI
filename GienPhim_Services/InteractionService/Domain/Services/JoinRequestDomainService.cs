using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Exceptions.Permission;
using GienPhim_Services.InteractionService.Domain.Exceptions.State;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;

namespace GienPhim_Services.InteractionService.Domain.Services
{
    public class JoinRequestDomainService : IJoinRequestDomainService
    {
        public void EnsureCanCreateRequest(ChatGroup group, JoinRequest? existingPendingRequest, bool alreadyMember, DateTime now)
        {
            if (group is null)
                throw new InvalidJoinRequestException("Không thể gửi yêu cầu tham gia phòng chat không tồn tại.");

            // Phòng đã kết thúc / bị xoá
            if (group.IsDeleted || now > group.AutoDeleteAt || now > group.ShowEndTime)
                throw new InvalidJoinRequestException("Phòng chat đã kết thúc hoặc bị đóng, không thể gửi yêu cầu tham gia.");

            // Đã là member rồi thì không cần request nữa
            if (alreadyMember)
                throw new InvalidJoinRequestException("Bạn đã là thành viên của phòng chat này.");

            // Đã có request Pending
            if (existingPendingRequest is not null && existingPendingRequest.Status == "Pending")
                throw new DuplicateJoinRequestException(existingPendingRequest.RequestUserId, existingPendingRequest.GroupId);
        }

        public void EnsureCanProcessRequest(JoinRequest request, ChatGroup group, string actingUserId, DateTime now, string action)
        {
            if (request is null)
                throw new InvalidJoinRequestException("Yêu cầu tham gia không tồn tại.");

            if (group is null)
                throw new InvalidJoinRequestException("Phòng chat không tồn tại.");

            // Chỉ chủ phòng mới được xử lý request
            if (group.CreatedByUserId != actingUserId)
                throw new JoinRequestPermissionException("Chỉ chủ phòng mới có quyền duyệt hoặc từ chối yêu cầu tham gia.");

            // Request không còn ở trạng thái Pending
            if (!string.Equals(request.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                throw new JoinRequestStateException("Yêu cầu tham gia đã được xử lý trước đó.");

            if (group.IsDeleted || now > group.AutoDeleteAt || now > group.ShowEndTime)
                throw new InvalidJoinRequestException("Phòng chat đã kết thúc hoặc bị đóng, không thể xử lý yêu cầu tham gia.");

            // action phải là Accept / Reject
            if (action != "Accept" && action != "Reject")
                throw new InvalidJoinRequestException("Hành động xử lý yêu cầu không hợp lệ.");
        }
    }
}
