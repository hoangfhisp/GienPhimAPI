using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Invalid
{
    public class InvalidTokenException : BusinessException
    {
        public override int StatusCode => 401; // Unauthorized

        public InvalidTokenException()
            : base("Token không hợp lệ hoặc đã hết hạn.")
        {
        }
    }
}
