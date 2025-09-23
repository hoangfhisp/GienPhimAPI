using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserEmailException : DomainException
    {
        public InvalidUserEmailException(string? email)
            : base($"Email '{email}' không hợp lệ hoặc để trống!")
        {
        }
    }
}
