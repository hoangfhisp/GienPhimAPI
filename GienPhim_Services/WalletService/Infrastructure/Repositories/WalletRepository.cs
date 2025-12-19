using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Domain.Entities;
using GienPhim_Services.WalletService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.WalletService.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;

        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }

        // Lấy ví theo UserId
        public async Task<Wallet?> FindWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Wallet.FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
        }

        // Lấy ví theo walletId
        public async Task<Wallet?> FindWalletByWalletIdAsync(string walletId, CancellationToken cancellationToken = default)
        {
            return await _context.Wallet.FirstOrDefaultAsync(w => w.WalletId == walletId, cancellationToken);
        }

        // Thêm mới ví
        public async Task<Wallet> AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            await _context.Wallet.AddAsync(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            return wallet;
        }

        // Lấy ví theo UserId
        public async Task<Wallet> UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            _context.Wallet.Update(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            return wallet;
        }

        // Lấy ví theo UserId
        public async Task<bool> DeleteAsync(string walletId, CancellationToken cancellationToken = default)
        {
            var wallet = await _context.Wallet.FirstOrDefaultAsync(w => w.WalletId == walletId, cancellationToken);
            if (wallet == null)
            {
                return false;
            }
            _context.Wallet.Remove(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
