using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class UserNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public UserNotFoundException(string userName)
            : base($"Không tìm thấy người dùng với tài khoản: {userName}") { }
    }
}
