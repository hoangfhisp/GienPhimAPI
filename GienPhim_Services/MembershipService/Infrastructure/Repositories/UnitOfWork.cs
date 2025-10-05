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
            IMembershipPlanRepository membershipPlans)
        {
            _context = context;
            MembershipPlans = membershipPlans;
        }

        public IMembershipPlanRepository MembershipPlans { get; }

        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }

}
