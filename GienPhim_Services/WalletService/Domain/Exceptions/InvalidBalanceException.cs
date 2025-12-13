using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidBalanceException : DomainException
    {
        public InvalidBalanceException(string account) : base($"Số dư '{account ?? string.Empty}' không hợp lệ hoặc không được nhỏ hơn 0") { }
    }
}
