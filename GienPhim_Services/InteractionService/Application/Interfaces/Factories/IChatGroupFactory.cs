using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Factories
{
    public interface IChatGroupFactory
    {
        ChatGroup CreateChatGroup(ChatGroupCreateDTO dto, string currentUserId, DateTime nowUtc);
    }
}
