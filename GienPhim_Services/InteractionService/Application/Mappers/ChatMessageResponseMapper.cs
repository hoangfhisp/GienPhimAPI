using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class ChatMessageResponseMapper : IChatMessageResponseMapper
    {
        public ChatMessageResponseDTO ToResponseDto(ChatMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            return new ChatMessageResponseDTO
            {
                MessageId = message.MessageId,
                GroupId = message.GroupId,
                UserId = message.UserId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsSystemMessage = message.IsSystemMessage,
                LoaiTinNhan = message.LoaiTinNhan
            };
        }

        public List<ChatMessageResponseDTO> ToResponseDtoList(IEnumerable<ChatMessage> messages)
        {
            return messages.Select(ToResponseDto).ToList();
        }
    }
}
