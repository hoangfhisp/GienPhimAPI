using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class UserNotFoundByEmailException : BusinessException
    {
        public override int StatusCode => 404;
        public UserNotFoundByEmailException(string email) 
            : base($"Không tìm thấy người dùng với email: {email}") { }
    }
}
