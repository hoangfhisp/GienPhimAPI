using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class CreateUserFailedException : BusinessException
    {
        public override int StatusCode => 500;
        public CreateUserFailedException(string email) 
            : base($"Tạo thông tin người dùng {email} thất bại") { }
    }
}
