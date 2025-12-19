using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Invalid
{
    public class InvalidPasswordException : BusinessException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;
        public InvalidPasswordException()
            : base("Mật khẩu không hợp lệ không chính xác") { }
    }
}
