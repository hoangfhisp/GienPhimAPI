using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.NotFounds
{
    public class WalletNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public WalletNotFoundException(string walletId)
            : base($"Wallet có ID '{walletId}' không tồn tại.")
        {
        }
    }
}
