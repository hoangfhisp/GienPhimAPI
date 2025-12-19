using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserTypeLoginException : DomainException
    {
        public InvalidUserTypeLoginException(string? userType )
            : base($"Loại tài khoản '{userType ?? string.Empty}' không hợp lệ hoặc không được để trống!")
        {
        }
    }
}
