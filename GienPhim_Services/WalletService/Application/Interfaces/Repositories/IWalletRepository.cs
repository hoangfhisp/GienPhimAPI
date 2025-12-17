using GienPhim_Services.WalletService.Domain.Entities;
using GienPhim_Services.WalletService.Infrastructure.Data;

namespace GienPhim_Services.WalletService.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet?> FindWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Find wallet by UserId
        Task<Wallet?> FindWalletByWalletIdAsync(string walletId, CancellationToken cancellationToken = default); // Find wallet by WalletId
        Task<Wallet> AddAsync(Wallet wallet, CancellationToken cancellationToken = default); // Add new wallet
        Task<Wallet> UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default); // Update wallet
        Task<bool> DeleteAsync(string walletId, CancellationToken cancellationToken = default); // Delete wallet by Id
    }
}
