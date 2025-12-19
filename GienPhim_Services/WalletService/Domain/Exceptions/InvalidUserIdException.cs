using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidUserIdException : DomainException
    {
        public InvalidUserIdException() : base("User Id không hợp lệ hoặc để trống") { }
    }
}
