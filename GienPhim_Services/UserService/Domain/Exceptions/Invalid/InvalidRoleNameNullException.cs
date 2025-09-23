using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidRoleNameNullException : DomainException
    {
        public InvalidRoleNameNullException(string roleName)
            : base($"Tên vai trò '{roleName}' không được để trống!")
        {
        }
    }
}
