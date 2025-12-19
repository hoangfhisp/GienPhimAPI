using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.Invalids
{
    public class InvalidWalletBalanceException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public InvalidWalletBalanceException(string walletId)
            : base($"Số dư trong ví tiền với WalletId '{walletId}' không đủ để thực hiện giao dịch.")
        {
        }
    }
}
