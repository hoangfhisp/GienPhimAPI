using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Infrastructure.Data;

namespace GienPhim_Services.MembershipService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MembershipDbContext _context;

        public UnitOfWork
        (
            MembershipDbContext context,
            IMembershipPlanRepository membershipPlans,
            IPlanOptionRepository planOptions)
        {
            _context = context;
            MembershipPlans = membershipPlans;
            PlanOptions = planOptions;
        }

        public IMembershipPlanRepository MembershipPlans { get; }
        public IPlanOptionRepository PlanOptions { get; }

        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }

}
