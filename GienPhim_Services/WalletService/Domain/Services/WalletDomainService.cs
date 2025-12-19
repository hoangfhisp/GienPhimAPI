using GienPhim_Services.WalletService.Domain.Entities;
using GienPhim_Services.WalletService.Domain.Exceptions;
using GienPhim_Services.WalletService.Domain.Interfaces.Services;

namespace GienPhim_Services.WalletService.Domain.Services
{
    public class WalletDomainService : IWalletDomainService
    {
        // Đảm bảo có thể tạo wallet được
        public void EnsureCanCreate(Wallet wallet)
        {
            if (wallet == null) {
                throw new InvalidWalletException(wallet.WalletId);
            }

            if (string.IsNullOrWhiteSpace(wallet.UserId))
            {
                throw new InvalidUserIdException();
            }
            if (wallet.SoDu > 0 || wallet.SoDu < 0)
                throw new InvalidCreateBalanceException(wallet.UserId);
        }

        public void EnsureCanUpdate(Wallet wallet)
        {
            if (wallet == null)
            {
                throw new InvalidWalletException(wallet.WalletId);
            }
            if (string.IsNullOrWhiteSpace(wallet.UserId))
            {
                throw new InvalidUserIdException();
            }
            if(wallet.SoDu < 0)
            {
                throw new InvalidUpdateBalanceException(wallet.UserId);
            }
            if (wallet.IsActive())
            {
                throw new InvalidWalletStatusException(wallet.WalletId);
            }
        }

        public void EnsureCanCreateTransaction(WalletTransaction walletTransaction)
        {
            if(walletTransaction.WalletTransactionId == null || walletTransaction.WalletId == null)
            {
                throw new InvalidWalletTransactionException(walletTransaction.WalletTransactionId);
            }
            if(walletTransaction.SoDuSauGiaoDich < 0)
            {
                throw new InvalidWalletTransactionBalanceException(walletTransaction.WalletTransactionId);
            }
        }
    }
}
