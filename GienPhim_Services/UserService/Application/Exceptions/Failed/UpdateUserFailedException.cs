using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class UpdateUserFailedException : BusinessException
    {
        public override int StatusCode => 500;
        public UpdateUserFailedException(string userName)
        : base($"Cập nhật thông tin người dùng {userName} thất bại")
        {
        }
    }
}
