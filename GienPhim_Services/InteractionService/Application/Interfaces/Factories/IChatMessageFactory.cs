using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Factories
{
    public interface IChatMessageFactory
    {
        // Tạo tin nhắn người dùng gửi (IsSystemMessage = false).
        ChatMessage CreateUserMessage( ChatMessageCreateDTO dto, string currentUserId, DateTime nowUtc);

        // Tạo tin nhắn hệ thống (vd: A vừa tham gia phòng, B đã rời phòng,...).
        ChatMessage CreateSystemMessage(Guid groupId, string content, DateTime nowUtc, string? loaiTinNhan = "system");
    }
}
