using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicateEmailException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicateEmailException(string email)
            : base($"Email '{email}' đã tồn tại. Vui lòng nhập email khác.") { }
    }
}
