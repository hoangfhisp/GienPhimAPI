using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserHasNoRoleException : DomainException
    {
        public InvalidUserHasNoRoleException(string account)
            : base($"Tài khoản {account} không có vai trò, liên hệ quản trị viên!") { }
    }
}
