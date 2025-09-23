using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserException : DomainException
    {
        public InvalidUserException(string? user)
            : base($"người dùng '{user}' không hợp lệ !")
        {
        }
    }
}
