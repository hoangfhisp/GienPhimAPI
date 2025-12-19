using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Domain.Services.Interfaces
{
    public interface IChatGroupDomainService
    {
        void EnsureCanCreate(ChatGroup group);
        void EnsureCanUpdateSettings(ChatGroup group, string currentUserId, DateTime nowUtc);
        void EnsureCanRefreshInviteCode(ChatGroup group, string currentUserId, DateTime nowUtc);
        void EnsureCanDisableInviteCode(ChatGroup group, string currentUserId, DateTime nowUtc);
        void EnsureCanClose(ChatGroup group, string currentUserId);
    }
}
