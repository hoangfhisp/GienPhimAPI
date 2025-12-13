using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Infrastructure.Data;

namespace GienPhim_Services.WalletService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletDbContext _context;

        public UnitOfWork(WalletDbContext context, IWalletRepository wallet, IWalletTransactionRepository walletTransaction)
        {
            _context = context;
            Wallet = wallet;
            WalletTransaction = walletTransaction;

        }

        public IWalletRepository Wallet { get; }
        public IWalletTransactionRepository WalletTransaction { get; }


        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }

}
