using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicateResetPasswordException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicateResetPasswordException(string? email = "")
            : base($"Mật khẩu mới trùng với mật khẩu trước: {email}") { }
    }
}
