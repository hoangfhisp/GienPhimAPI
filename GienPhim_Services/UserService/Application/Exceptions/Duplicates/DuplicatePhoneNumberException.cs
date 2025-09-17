using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicatePhoneNumberException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicatePhoneNumberException(string phoneNumber)
            : base($"Số điện thoại '{phoneNumber}' đã tồn tại. Vui lòng nhập số điện thoại khác.") { }
    }
}
