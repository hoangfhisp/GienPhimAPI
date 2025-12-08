using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Domain.Services.Interfaces
{
    public interface IJoinRequestDomainService
    {
        // Khi user gửi request mới
        void EnsureCanCreateRequest(ChatGroup group, JoinRequest? existingPendingRequest, bool alreadyMember, DateTime now);

        // Khi chủ group xử lý request (Accept/Reject)
        void EnsureCanProcessRequest(JoinRequest request, ChatGroup group, string actingUserId, DateTime now, string action); // "Accept" / "Reject"
    }
}
