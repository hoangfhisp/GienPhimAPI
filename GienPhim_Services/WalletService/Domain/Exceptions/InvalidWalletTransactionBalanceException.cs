using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidWalletTransactionBalanceException : DomainException
    {
        public InvalidWalletTransactionBalanceException(string walletTransactionId) : base($"Số dư hoặc số tiền thay đổi trong giao dịch ví '{walletTransactionId ?? string.Empty}' không hợp lệ") { }
    }
}
