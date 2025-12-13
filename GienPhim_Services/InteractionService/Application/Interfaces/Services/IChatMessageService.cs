using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IChatMessageService
    {
        // User gửi tin nhắn vào phòng chat
        Task<ResultDTO<ChatMessageResponseDTO>> SendMessageAsync(ChatMessageCreateDTO dto, CancellationToken cancellationToken = default);

        // Lấy danh sách tin nhắn trong 1 phòng chat (phân trang)
        Task<ResultListDTO<List<ChatMessageResponseDTO>>> GetMessagesByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
