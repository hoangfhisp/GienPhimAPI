using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.Duplicates
{
    public class DuplicateWalletUserIdException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateWalletUserIdException(string userId)
            : base($"Wallet UserId '{userId}' đã tồn tại , không thể tạo wallet cho người dùng này.")
        {
        }
    }
}
