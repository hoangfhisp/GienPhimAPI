using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.NotFounds
{
    public class UserEmailNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public UserEmailNotFoundException(string? email)
            : base($"Không tìm thấy người dùng với email: {email ?? "unknown"}")
        {
        }
    }
}
