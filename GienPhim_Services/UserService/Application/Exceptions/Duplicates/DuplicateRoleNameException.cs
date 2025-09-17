using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicateRoleNameException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicateRoleNameException(string roleName)
            : base($"Tên vai trò '{roleName}' đã tồn tại.") { }
    }
}
