using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IJoinRequestService
    {
        // User gửi yêu cầu tham gia phòng chat
        Task<ResultDTO<JoinRequestResponseDTO>> CreateJoinRequestAsync(JoinRequestCreateDTO dto, CancellationToken cancellationToken = default);

        // Chủ phòng lấy danh sách yêu cầu theo group (có thể filter theo status)
        Task<ResultListDTO<List<JoinRequestResponseDTO>>> GetByGroupAsync(Guid groupId, string? status, int page, int pageSize, CancellationToken cancellationToken = default);

        // Chủ phòng chấp nhận yêu cầu
        Task<ResultDTO<JoinRequestResponseDTO>> AcceptAsync(Guid requestId, CancellationToken cancellationToken = default);

        // Chủ phòng từ chối yêu cầu
        Task<ResultDTO<JoinRequestResponseDTO>> RejectAsync(Guid requestId, CancellationToken cancellationToken = default);
    }
}
