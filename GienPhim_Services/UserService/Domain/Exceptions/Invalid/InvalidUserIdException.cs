using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserIdException : DomainException
    {
        public InvalidUserIdException(string? userId)
            : base($"UserId '{userId}' không hợp lệ hoặc để trống!")
        {
        }
    }
}
