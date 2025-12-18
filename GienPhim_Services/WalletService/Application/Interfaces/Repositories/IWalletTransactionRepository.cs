using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Repositories
{
    public interface IWalletTransactionRepository
    {
        Task<(List<WalletTransaction> walletTransactions, int total, int last)> GetByWalletIdAsync(string walletId, int page, int limit, CancellationToken cancellationToken = default); // Lấy giao dịch ví tiền từ userId
        Task<WalletTransaction?> GetByIdAsync(string walletTransactionId, CancellationToken cancellationToken = default); // Lấy giao dịch ví tiền từ Id
        Task<WalletTransaction?> CreateAsync(WalletTransaction walletTransaction, CancellationToken cancellationToken = default); // Thêm mới giao dịch ví tiền
        Task<WalletTransaction?> UpdateAsync(WalletTransaction walletTransaction, CancellationToken cancellationToken = default); // Update giao dịch ví tiền
        Task<bool> DeleteAsync(string walletTransactionId, CancellationToken cancellationToken = default); // Xóa giao dịch ví tiền
        Task<WalletTransaction?> GetByMoTaAsync(string mota, CancellationToken cancellationToken = default); // Lấy giao dịch ví tiền từ mô tả
    }
}
