using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IChatMessageResponseMapper
    {
        ChatMessageResponseDTO ToResponseDto(ChatMessage message);

        List<ChatMessageResponseDTO> ToResponseDtoList(IEnumerable<ChatMessage> messages);
    }
}
