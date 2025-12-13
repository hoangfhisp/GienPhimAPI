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
    }
}
