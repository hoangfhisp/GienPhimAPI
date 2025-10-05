using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Repositories
{
    public interface IPlanOptionRepository
    {
        Task<(List<PlanOption> options, int total, int last)> GetAllPlanOptionsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<PlanOption?> GetByIdAsync(string optionId, CancellationToken ct = default);
        Task<PlanOption> CreateAsync(PlanOption option, CancellationToken ct = default);
        Task<PlanOption> UpdateAsync(PlanOption option, CancellationToken ct = default);
        Task DeleteAsync(PlanOption option, CancellationToken ct = default);
    }
}
