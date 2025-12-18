using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.Failed
{
    public class UpdateWalletTransactionFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public UpdateWalletTransactionFailedException()
            : base("Cập nhật giao dịch ví thất bại.")
        {
        }
    }
}
