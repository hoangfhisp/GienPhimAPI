using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Orchestrators
{
    public interface IWalletOrchestrator
    {
        Task<Wallet> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default); // Tạo ví mới
    }
}
