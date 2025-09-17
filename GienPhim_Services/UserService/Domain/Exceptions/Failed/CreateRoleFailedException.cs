using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public class CreateRoleFailedException : DomainException
    {
        public CreateRoleFailedException(string roleName) 
            : base($"Tạo role '{roleName}' mới thất bại.")
        {
        }
    }
}
