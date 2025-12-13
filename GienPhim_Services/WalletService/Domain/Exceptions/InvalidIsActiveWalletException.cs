using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidIsActiveWalletException : DomainException
    {
        public InvalidIsActiveWalletException() : base("Ví đã và đang hoạt động.")
        {
        }
    }
}
