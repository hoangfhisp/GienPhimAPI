using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidChangeEmailException : DomainException
    {
        public InvalidChangeEmailException(string email) : base($"Bạn chỉ có thể thay đổi email '{email}' sau 30 ngày thay đổi!")
        {
        }
    }
}
