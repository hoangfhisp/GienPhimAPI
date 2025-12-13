using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Factories
{
    public interface IGroupMemberFactory
    {
        GroupMember CreateForJoin(ChatGroup group, string userId, DateTime joinedAt, bool isOwner = false);
    }
}
