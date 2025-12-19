using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Interfaces.Factories;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Factories
{
    public class WalletFactory : IWalletFactory
    { 
        public Wallet CreateWallet (CreateWalletRequestDTO dto)
        {
            return new Wallet
            {
                WalletId = Guid.NewGuid().ToString("N"),
                UserId = dto.UserId,
                SoDu = dto.SoDu,
                TrangThaiVi = true,
                NgayTao = DateTime.UtcNow,
                NgayCapNhat = DateTime.UtcNow

            };
        }
        public Wallet UpdateWallet(UpdateWalletRequestDTO dto)
        {
            return new Wallet
            {
                UserId = dto.UserId,
                SoDu = dto.SoDu,
                NgayCapNhat = DateTime.UtcNow

            };
        }
    }
}
