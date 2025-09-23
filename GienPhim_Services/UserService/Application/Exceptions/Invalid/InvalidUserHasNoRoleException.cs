using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Invalid
{
    public class InvalidUserHasNoRoleException : BusinessException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;
        public InvalidUserHasNoRoleException(string account)
            : base($"Tài khoản {account} không có vai trò, liên hệ quản trị viên!") { }
    }
}
