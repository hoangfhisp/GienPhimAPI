using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Invalid
{
    public class InvalidRefreshTokenException : DomainException
    {
        public InvalidRefreshTokenException() : base("Refresh token không hợp lệ hoặc để trống")
        {
        }
    }
}
