using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Invalid
{
    public class InvalidTokenException : DomainException
    {
        public InvalidTokenException() 
            : base("Token không hợp lệ hoặc đã hết hạn.") {}
    }
}
