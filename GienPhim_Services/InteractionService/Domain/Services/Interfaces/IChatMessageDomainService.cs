using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Domain.Services.Interfaces
{
    public interface IChatMessageDomainService
    {
        void EnsureCanSend(ChatGroup group, GroupMember? member, string content, bool isSystemMessage, DateTime nowUtc);
    }
}
