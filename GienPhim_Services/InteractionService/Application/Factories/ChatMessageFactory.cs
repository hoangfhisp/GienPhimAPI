using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Factories
{
    public class ChatMessageFactory : IChatMessageFactory
    {
        public ChatMessage CreateUserMessage(ChatMessageCreateDTO dto, string currentUserId, DateTime nowUtc)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (dto.GroupId == Guid.Empty) throw new ArgumentException("GroupId không được để trống.", nameof(dto.GroupId));

            if (string.IsNullOrWhiteSpace(currentUserId)) throw new ArgumentException("User hiện tại không hợp lệ.", nameof(currentUserId));

            var content = (dto.Content ?? string.Empty).Trim();

            return new ChatMessage
            {
                MessageId = Guid.NewGuid(),
                GroupId = dto.GroupId,
                UserId = currentUserId,
                Content = content,
                SentAt = nowUtc,
                IsSystemMessage = false,
                LoaiTinNhan = string.IsNullOrWhiteSpace(dto.LoaiTinNhan) ? "text" : dto.LoaiTinNhan
            };
        }

        public ChatMessage CreateSystemMessage(Guid groupId, string content, DateTime nowUtc, string? loaiTinNhan = "system")
        {
            if (groupId == Guid.Empty) throw new ArgumentException("GroupId không được để trống.", nameof(groupId));

            var v = (content ?? string.Empty).Trim();

            return new ChatMessage
            {
                MessageId = Guid.NewGuid(),
                GroupId = groupId,
                UserId = string.Empty,  
                Content = v,
                SentAt = nowUtc,
                IsSystemMessage = true,
                LoaiTinNhan = loaiTinNhan
            };
        }
    }
}
