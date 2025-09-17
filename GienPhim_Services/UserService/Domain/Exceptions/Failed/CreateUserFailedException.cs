using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public class CreateUserFailedException : DomainException
    {
        public CreateUserFailedException(string email) 
            : base($"Cập nhật thông tin người dùng {email} thất bại") { }
    }
}
