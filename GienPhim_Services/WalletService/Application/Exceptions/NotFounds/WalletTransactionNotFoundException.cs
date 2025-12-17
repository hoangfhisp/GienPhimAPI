using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.NotFounds
{
    public class WalletTransactionNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public WalletTransactionNotFoundException(string walletId)
            : base($"Không tìm thấy giao dịch ví tiền cho WalletId '{walletId}'.")
        {
        }
    }
}
