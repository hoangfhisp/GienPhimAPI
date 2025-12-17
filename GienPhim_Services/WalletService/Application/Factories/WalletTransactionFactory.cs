using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Interfaces.Factories;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Factories
{
    public class WalletTransactionFactory : IWalletTransactionFactory
    {
        public WalletTransaction Create(CreateWalletTransactionRequestDTO dto, decimal soDuSauGiaoDich)
        {
            return new WalletTransaction
            {
                WalletTransactionId = Guid.NewGuid().ToString("N"),
                WalletId = dto.WalletId,
                LoaiGiaoDich = dto.LoaiGiaoDich,
                SoTienThayDoi = dto.SoTienThayDoi,
                SoDuSauGiaoDich = soDuSauGiaoDich,
                MoTa = dto.MoTa,
                NgayGiaoDich = DateTime.UtcNow,
                TrangThai = "Thành công" // Mặc định trạng thái là thành công
            };
        }
    }
}
