using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserNameException : BusinessException
    {
        public InvalidUserNameException(string? userName)
            : base($"Tên người dùng '{userName}' không hợp lệ hoặc để trống!")
        {
        }
    }
}
