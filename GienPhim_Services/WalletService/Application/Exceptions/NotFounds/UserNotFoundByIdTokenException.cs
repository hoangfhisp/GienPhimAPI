using GienPhim_Services.WalletService.Shared.Exceptions;

namespace GienPhim_Services.WalletService.Application.Exceptions.NotFounds
{
    public class UserNotFoundByIdTokenException : BusinessException
    {
        public override int StatusCode => 404;
        public UserNotFoundByIdTokenException()
            : base($"Không tìm thấy id người dùng từ token")
        {
        }
    }
}
