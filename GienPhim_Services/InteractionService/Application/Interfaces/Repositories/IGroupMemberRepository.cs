using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IGroupMemberRepository
    {
        Task<GroupMember?> GetByIdAsync(Guid groupMemberId, CancellationToken cancellationToken = default);

        Task<GroupMember?> GetByGroupAndUserAsync(Guid groupId,string userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<GroupMember>> GetByGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<GroupMember> Items, int Total, int LastPage)> GetByGroupPagedAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default);

        Task<bool> IsLastOwnerAsync(Guid groupId, string userId, CancellationToken cancellationToken = default);

        Task AddAsync(GroupMember member, CancellationToken cancellationToken = default);

        Task UpdateAsync(GroupMember member, CancellationToken cancellationToken = default);

        Task RemoveAsync(GroupMember member, CancellationToken cancellationToken = default);
    }
}
