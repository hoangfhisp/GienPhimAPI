using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.MembershipService.Infrastructure.Repositories
{
    public class MembershipPlanRepository : IMembershipPlanRepository
    {
        private readonly MembershipDbContext _membershipContext;

        public MembershipPlanRepository(MembershipDbContext membershipContext)
        {
            _membershipContext = membershipContext;
        }

        // Lấy tất cả MembershipPlan có phân trang
        public async Task<(List<MembershipPlan> plans, int total, int last)> GetAllMembershipPlansAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _membershipContext.MembershipPlans.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var plans = await query
                .OrderBy(p => p.PlanId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (plans, total, last);
        }

        // Lấy theo Id
        public async Task<MembershipPlan?> GetByIdAsync(string planId, CancellationToken cancellationToken = default)
        {
            return await _membershipContext.MembershipPlans
                .FirstOrDefaultAsync(p => p.PlanId == planId, cancellationToken);
        }

        // Tìm theo tên (phục vụ check trùng tên)
        public async Task<MembershipPlan?> FindByNameAsync(string tenPlan, CancellationToken cancellationToken = default)
        {
            return await _membershipContext.MembershipPlans
                .FirstOrDefaultAsync(p => p.TenPlan.ToLower() == tenPlan.ToLower(), cancellationToken);
        }

        // Tạo mới
        public async Task<MembershipPlan> CreateAsync(MembershipPlan plan, CancellationToken cancellationToken = default)
        {
            _membershipContext.MembershipPlans.Add(plan);
            await _membershipContext.SaveChangesAsync(cancellationToken);
            return plan;
        }

        // Cập nhật
        public async Task<MembershipPlan> UpdateAsync(MembershipPlan plan, CancellationToken cancellationToken = default)
        {
            _membershipContext.MembershipPlans.Update(plan);
            await _membershipContext.SaveChangesAsync(cancellationToken);
            return plan;
        }

        // Xoá
        public async Task DeleteAsync(MembershipPlan plan, CancellationToken cancellationToken = default)
        {
            _membershipContext.MembershipPlans.Remove(plan);
            await _membershipContext.SaveChangesAsync(cancellationToken);
        }
    }
}
