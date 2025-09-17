using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public partial class AssignRoleFailedException : DomainException
    {
        public AssignRoleFailedException(string userName, string roleName)
            : base($"Gán vai trò '{roleName}' cho người dùng '{userName}' thất bại.") { }
    }
}
