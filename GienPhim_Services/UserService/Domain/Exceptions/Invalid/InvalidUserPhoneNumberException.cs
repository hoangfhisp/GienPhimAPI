using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserPhoneNumberException : BusinessException
    {
        public InvalidUserPhoneNumberException(string? phoneNumber)
            : base($"Số điện thoại '{phoneNumber}' không hợp lệ hoặc để trống!")
        {
        }
    }
}
