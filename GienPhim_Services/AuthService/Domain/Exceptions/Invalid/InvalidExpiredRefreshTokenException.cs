using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Invalid
{
    public class InvalidExpiredRefreshTokenException : DomainException
    {
        public InvalidExpiredRefreshTokenException() : base("Refresh token đã hết hạn hoặc bị khóa")
        {
        }
    }
}
