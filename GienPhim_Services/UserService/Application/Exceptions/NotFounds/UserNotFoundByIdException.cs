using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class UserNotFoundByIdException : BusinessException 
    {
        public override int StatusCode => 404;
        public UserNotFoundByIdException(string id)
            : base($"Không tìm thấy người dùng với id: {id}") { }
    }
}
