namespace GienPhim_Services.MembershipService.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IMembershipPlanRepository MembershipPlans { get; }
        IPlanOptionRepository PlanOptions { get; }
        IUserMembershipRepository UserMemberships { get; }
        Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default); // Transaction
    }
}
