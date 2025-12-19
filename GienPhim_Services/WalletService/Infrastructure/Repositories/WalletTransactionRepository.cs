using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Domain.Entities;
using GienPhim_Services.WalletService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.WalletService.Infrastructure.Repositories
{
    public class WalletTransactionRepository : IWalletTransactionRepository
    {
        private readonly WalletDbContext _context;

        public WalletTransactionRepository(WalletDbContext context)
        {
            _context = context;
        }

        // Lấy giao dịch ví tiền từ userId
        public async Task<(List<WalletTransaction> walletTransactions, int total, int last)> GetByWalletIdAsync(string walletId, int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.WalletTransaction.Where(wt => wt.WalletId == walletId).AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var walletTransactions = await query
                .OrderBy(p => p.NgayGiaoDich)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (walletTransactions, total, last);
        }

        // Lấy giao dịch ví tiền từ Id
        public async Task<WalletTransaction?> GetByIdAsync(string walletTransactionId, CancellationToken cancellationToken = default)
        {
            return await _context.WalletTransaction.FirstOrDefaultAsync(wt => wt.WalletTransactionId == walletTransactionId, cancellationToken);
        }

        // Lấy giao dịch ví tiền từ Mô Tả
        public async Task<WalletTransaction?> GetByMoTaAsync(string mota, CancellationToken cancellationToken = default)
        {
            return await _context.WalletTransaction.FirstOrDefaultAsync(wt => wt.MoTa == mota, cancellationToken);
        }

        // Thêm mới giao dịch ví tiền
        public async Task<WalletTransaction?> CreateAsync(WalletTransaction walletTransaction, CancellationToken cancellationToken = default)
        {
            await _context.WalletTransaction.AddAsync(walletTransaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return walletTransaction;
        }

        // Update giao dịch ví tiền
        public async Task<WalletTransaction?> UpdateAsync(WalletTransaction walletTransaction, CancellationToken cancellationToken = default)
        {
            _context.WalletTransaction.Update(walletTransaction);
            await _context.SaveChangesAsync(cancellationToken);
            return walletTransaction;
        }

        // Xóa giao dịch ví tiền
        public async Task<bool> DeleteAsync(string walletTransactionId, CancellationToken cancellationToken = default)
        {
            var walletTransaction = await _context.WalletTransaction.FirstOrDefaultAsync(wt => wt.WalletTransactionId == walletTransactionId, cancellationToken);
            if (walletTransaction == null)
            {
                return false;
            }
            _context.WalletTransaction.Remove(walletTransaction);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
