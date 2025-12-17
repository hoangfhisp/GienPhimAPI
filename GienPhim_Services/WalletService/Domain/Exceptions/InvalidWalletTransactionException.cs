using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidWalletTransactionException : DomainException
    {
        public InvalidWalletTransactionException(string walletTransactionId) : base($"Giao dịch ví '{walletTransactionId ?? string.Empty}' không hợp lệ hoặc không tồn tại") { }
    }
}
