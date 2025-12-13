using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidCreateBalanceException : DomainException
    {
        public InvalidCreateBalanceException(string? account) : base($"Số dư khởi tạo '{account ?? string.Empty}' không hợp lệ , số dư khởi tạo phải bằng 0") { }
    }
}
