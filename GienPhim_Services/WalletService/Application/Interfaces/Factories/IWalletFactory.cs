using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Factories
{
    public interface IWalletFactory
    {
        Wallet CreateWallet(CreateWalletRequestDTO dto); // Tạo ví mới từ DTO
        Wallet UpdateWallet(UpdateWalletRequestDTO dto); // Cập nhật ví từ DTO
    }
}
