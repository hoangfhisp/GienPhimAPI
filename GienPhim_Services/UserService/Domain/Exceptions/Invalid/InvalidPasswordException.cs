using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidPasswordException : DomainException
    {
        public InvalidPasswordException()
            : base("Mật khẩu không hợp lệ không chính xác") { }
    }
}
