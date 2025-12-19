using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Interfaces.Mappers;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Mappers
{
    public class WalletResponseMapper : IWalletResponseMapper
    {
        // Chuyển đổi từ Wallet entity sang WalletResponseDTO
        public WalletResponseDTO ToDTO (Wallet wallet)
        {
            return new WalletResponseDTO
            {
                WalletId = wallet.WalletId,
                SoDu = wallet.SoDu,
                TrangThaiVi = wallet.TrangThaiVi,
                NgayTao = wallet.NgayTao,
                NgayCapNhat = wallet.NgayCapNhat
            };
        }

        public WalletTransactionResponseDTO ToTransactionDTO(WalletTransaction walletTransaction)
        {
            return new WalletTransactionResponseDTO
            {
                WalletTransactionId = walletTransaction.WalletTransactionId,
                LoaiGiaoDich = walletTransaction.LoaiGiaoDich,
                SoTienThayDoi = walletTransaction.SoTienThayDoi,
                SoDuSauGiaoDich = walletTransaction.SoDuSauGiaoDich,
                MoTa = walletTransaction.MoTa,
                NgayGiaoDich = walletTransaction.NgayGiaoDich,
                TrangThai = walletTransaction.TrangThai,
            };
        }
    }
}
