using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public class UpdateUserFailedException : DomainException
    {
        public UpdateUserFailedException(string userName)
        : base($"Cập nhật thông tin người dùng {userName} thất bại")
        {
        }
    }
}
