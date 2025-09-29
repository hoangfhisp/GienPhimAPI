using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.NotFounds
{
    public class UserNotFoundByIdException : BusinessException
    {
        public override int StatusCode => 404;

        public UserNotFoundByIdException(string? userId)
            : base($"Không tìm thấy người dùng với ID: {userId ?? "nuknown"}")
        {
        }
    }
}
