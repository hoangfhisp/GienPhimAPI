using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.Failed
{
    public class UpdateWalletFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public UpdateWalletFailedException(string walletId)
            : base($"Cập nhật ví tiền với WalletId '{walletId}' thất b��i.")
        {
        }
    }
}
