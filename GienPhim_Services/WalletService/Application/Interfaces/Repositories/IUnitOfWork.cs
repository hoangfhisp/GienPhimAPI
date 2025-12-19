namespace GienPhim_Services.WalletService.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IWalletRepository Wallet { get; }
        public IWalletTransactionRepository WalletTransaction { get; }
        Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default); // Transaction
    }
}
