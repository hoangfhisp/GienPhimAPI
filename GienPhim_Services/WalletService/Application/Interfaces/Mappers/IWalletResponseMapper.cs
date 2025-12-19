using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Mappers
{
    public interface IWalletResponseMapper
    {
        WalletResponseDTO ToDTO(Wallet wallet); // Chuyển đổi từ Wallet entity sang WalletResponseDTO
        WalletTransactionResponseDTO ToTransactionDTO(WalletTransaction walletTransaction); // Chuyển đổi từ WalletTransaction entity sang WalletTransactionResponseDTO
    }
}
