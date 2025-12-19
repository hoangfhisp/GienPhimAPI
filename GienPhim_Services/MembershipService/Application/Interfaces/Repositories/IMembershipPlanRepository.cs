using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Repositories
{
    public interface IMembershipPlanRepository
    {
        Task<(List<MembershipPlan> plans, int total, int last)> GetAllMembershipPlansAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<MembershipPlan?> GetByIdAsync(string planId, CancellationToken cancellationToken = default);
        Task<MembershipPlan?> FindByNameAsync(string tenPlan, CancellationToken cancellationToken = default);
        Task<MembershipPlan> CreateAsync(MembershipPlan plan, CancellationToken cancellationToken = default);
        Task<MembershipPlan> UpdateAsync(MembershipPlan plan, CancellationToken cancellationToken = default);
        Task DeleteAsync(MembershipPlan plan, CancellationToken cancellationToken = default);
    }
}
