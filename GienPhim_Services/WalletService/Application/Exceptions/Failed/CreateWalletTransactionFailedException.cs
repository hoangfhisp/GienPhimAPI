using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.Failed
{
    public class CreateWalletTransactionFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public CreateWalletTransactionFailedException(string walletId)
            : base($"Tạo giao dịch ví thất bại cho walletId = {walletId}")
        {
        }
    }
}
