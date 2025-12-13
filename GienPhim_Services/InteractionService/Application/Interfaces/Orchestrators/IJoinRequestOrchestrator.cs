using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators
{
    public interface IJoinRequestOrchestrator
    {
        // Tạo yêu cầu tham gia mới
        Task<JoinRequest> CreateJoinRequestAsync(ChatGroup group, JoinRequestCreateDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // Chấp nhận yêu cầu (trả về GroupMember nếu cần tạo mới)
        Task<GroupMember?> AcceptAsync(JoinRequest request, ChatGroup group, string actingUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // Từ chối yêu cầu
        Task RejectAsync(JoinRequest request, ChatGroup group, string actingUserId, DateTime nowUtc, CancellationToken cancellationToken = default);
    }
}
