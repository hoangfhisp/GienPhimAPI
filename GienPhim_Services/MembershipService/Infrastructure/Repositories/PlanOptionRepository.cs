using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.MembershipService.Infrastructure.Repositories
{
    public class PlanOptionRepository : IPlanOptionRepository
    {
        private readonly MembershipDbContext _membershipContext;

        public PlanOptionRepository(MembershipDbContext membershipContext)
        {
            _membershipContext = membershipContext;
        }

        // Lấy danh sách PlanOption có phân trang
        public async Task<(List<PlanOption> options, int total, int last)> GetAllPlanOptionsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _membershipContext.PlanOptions
                .Include(o => o.MembershipPlan) // load plan kèm option (nếu cần)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var options = await query
                .OrderBy(o => o.OptionId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (options, total, last);
        }

        public async Task<(List<PlanOption> options, int total, int last)> GetPlanOptionsByPlanIdAndDayAsync(string plainId, int day ,int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _membershipContext.PlanOptions
                .Include(o => o.MembershipPlan) // load plan kèm option (nếu cần)
                .Where(o => o.PlanId == plainId && o.ThoiGianSuDung == day)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var options = await query
                .OrderBy(o => o.OptionId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (options, total, last);
        }

        // Lấy PlanOption theo Id
        public async Task<PlanOption?> GetByIdAsync(string optionId, CancellationToken ct = default)
        {
            return await _membershipContext.PlanOptions
                .Include(o => o.MembershipPlan)
                .FirstOrDefaultAsync(o => o.OptionId == optionId, ct);
        }

        // Tạo mới PlanOption
        public async Task<PlanOption> CreateAsync(PlanOption option, CancellationToken ct = default)
        {
            _membershipContext.PlanOptions.Add(option);
            await _membershipContext.SaveChangesAsync(ct); // tự commit
            return option;
        }

        // Cập nhật PlanOption
        public async Task<PlanOption> UpdateAsync(PlanOption option, CancellationToken ct = default)
        {
            _membershipContext.PlanOptions.Update(option);
            await _membershipContext.SaveChangesAsync(ct);
            return option;
        }

        // Xóa PlanOption
        public async Task DeleteAsync(PlanOption option, CancellationToken ct = default)
        {
            _membershipContext.PlanOptions.Remove(option);
            await _membershipContext.SaveChangesAsync(ct);
        }
    }
}
