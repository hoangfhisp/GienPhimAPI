using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidWalletStatusException : DomainException
    {
        public InvalidWalletStatusException(string walletId)
            : base($"Ví có id '{walletId}' ví đang bị khóa.")
        {
        }
    }
}
