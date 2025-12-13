using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidWalletException : DomainException
    {
        public InvalidWalletException(string walletId) : base($"Ví '{walletId ?? string.Empty}' không hợp lệ hoặc không tồn tại") { }
    }
}
