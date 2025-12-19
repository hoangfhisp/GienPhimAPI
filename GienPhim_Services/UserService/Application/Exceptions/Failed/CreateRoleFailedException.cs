using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class CreateRoleFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public CreateRoleFailedException(string roleName) 
            : base($"Tạo role '{roleName}' mới thất bại.")
        {
        }
    }
}
