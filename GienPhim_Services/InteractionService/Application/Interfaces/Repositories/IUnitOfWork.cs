namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IChatGroupRepository ChatGroups { get; }
        IGroupMemberRepository GroupMembers { get; }

        Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default); // Transaction
    }
}
