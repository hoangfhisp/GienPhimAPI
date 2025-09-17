using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class UserNotFoundByUserNameException : BusinessException
    {
        public override int StatusCode => 404;
        public UserNotFoundByUserNameException(string userName)
            : base($"Không tìm thấy người dùng với tên đăng nhập: {userName}") { }
    }
}
