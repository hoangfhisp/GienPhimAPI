using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Duplicate
{
    public class DuplicateResetPasswordException : DomainException
    {
        public DuplicateResetPasswordException(string? email = "")
            : base($"Mật khẩu mới trùng với mật khẩu trước: {email}") { }
    }
}
