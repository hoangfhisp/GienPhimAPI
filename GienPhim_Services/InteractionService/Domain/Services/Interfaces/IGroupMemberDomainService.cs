using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Domain.Services.Interfaces
{
    public interface IGroupMemberDomainService
    {
        void EnsureCanJoin(ChatGroup group, GroupMember? existingMembership, DateTime now);
        void EnsureCanLeave(ChatGroup group, GroupMember member, bool isLastOwner);
        void EnsureCanTransferOwnership(ChatGroup group, GroupMember currentOwner, GroupMember newOwner);
        void EnsureCanUpdateMemberPermissions(ChatGroup group,GroupMember actingMember,GroupMember targetMember);
    }
}
