using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Invalid
{
    public class InvalidRefreshTokenException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public InvalidRefreshTokenException() : base("Refresh token không hợp lệ hoặc để trống")
        {
        }
    }
}
