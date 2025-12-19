using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IChatGroupResponseMapper
    {
        ChatGroupResponseDTO ToResponseDto(ChatGroup group);
        List<ChatGroupResponseDTO> ToResponseDtoList(List<ChatGroup> groups);

        ChatGroupListItemDTO ToListItemDto(ChatGroup group);
        List<ChatGroupListItemDTO> ToListItemDtoList(List<ChatGroup> groups);
    }
}
