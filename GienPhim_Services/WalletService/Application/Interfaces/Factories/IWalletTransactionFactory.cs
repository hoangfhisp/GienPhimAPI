using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Factories
{
    public interface IWalletTransactionFactory
    {
        WalletTransaction Create(CreateWalletTransactionRequestDTO dto, decimal soDuSauGiaoDich); // Tạo giao dịch ví tiền mới từ DTO
    }
}
