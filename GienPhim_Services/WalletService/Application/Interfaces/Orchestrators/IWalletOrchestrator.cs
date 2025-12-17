using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.User;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Application.Interfaces.Orchestrators
{
    public interface IWalletOrchestrator
    {
        Task<Wallet> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default); // Tạo ví mới
        Task<Wallet> UpdateWalletAsync(UpdateWalletRequestDTO dto, CancellationToken cancellationToken = default); // Cập nhật ví
        Task<WalletTransaction> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO dto, CancellationToken cancellationToken = default); // Tạo giao dịch ví mới
        Task<ResultDTO<UserHttpResponseDTO>?> FindUserByIdAsync(string userId, CancellationToken cancellationToken = default); // Tìm user theo id bằng UserService
    }
}
