using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicateUsernameException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicateUsernameException(string username)
            : base($"UserName '{username}' đã tồn tại. Vui lòng nhập UserName khác.") { }
    }
}
