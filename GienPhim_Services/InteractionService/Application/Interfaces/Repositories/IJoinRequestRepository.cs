using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IJoinRequestRepository
    {
        Task<JoinRequest?> GetByIdAsync(Guid requestId, CancellationToken cancellationToken = default);

        Task<JoinRequest?> GetPendingByGroupAndUserAsync(Guid groupId, string userId, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<JoinRequest> Items, int Total, int LastPage)> GetByGroupAsync(Guid groupId, string? status, int page, int pageSize, CancellationToken cancellationToken = default);

        Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default);

        Task UpdateAsync(JoinRequest request, CancellationToken cancellationToken = default);
    }
}
