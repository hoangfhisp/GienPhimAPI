using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators
{
    public interface IChatMessageOrchestrator
    {
        // Dựng tin nhắn user gửi, kiểm tra rule domain (quyền, trạng thái phòng, nội dung, ...).
        Task<ChatMessage> CreateUserMessageAsync(ChatGroup group, GroupMember? member, ChatMessageCreateDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        // Dựng tin nhắn hệ thống (không cần member, nhưng vẫn có thể check trạng thái group).
        Task<ChatMessage> CreateSystemMessageAsync(ChatGroup group, string content, DateTime nowUtc, string? loaiTinNhan = "system", CancellationToken cancellationToken = default);
    }
}
