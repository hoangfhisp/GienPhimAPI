using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Duplicate
{
    public class DuplicateRoleNameException : DomainException
    {
        public DuplicateRoleNameException(string? roleName)
            : base($"Role name '{roleName ?? string.Empty}' đã tồn tại.")
        {
        }
    }
}
