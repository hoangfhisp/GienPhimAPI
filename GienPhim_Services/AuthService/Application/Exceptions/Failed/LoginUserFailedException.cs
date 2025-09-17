using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class LoginUserFailedException : BusinessException
    {
        public override int StatusCode { get; } // Unauthorized
        public LoginUserFailedException(string message , int statusCode = 500)
            : base($"Đăng nhập thất bại: {message}")
        {
            StatusCode = statusCode;
        }
    }
}
