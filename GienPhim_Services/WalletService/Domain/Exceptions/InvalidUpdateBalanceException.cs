using GienPhim_Services.WalletService.shared.Exceptions;

namespace GienPhim_Services.WalletService.Domain.Exceptions
{
    public class InvalidUpdateBalanceException : DomainException
    {
        public InvalidUpdateBalanceException(string userId) : base($"Cập nhật số dư không hợp lệ cho người dùng '{userId ?? string.Empty}'") { }
    }
}
