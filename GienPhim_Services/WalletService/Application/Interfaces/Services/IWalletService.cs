using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;

namespace GienPhim_Services.WalletService.Application.Interfaces.Services
{
    public interface IWalletService
    {
        Task<ResultDTO<WalletResponseDTO>> GetWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Lấy thông tin ví
        Task<ResultDTO<WalletResponseDTO>> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default); // Tạo ví mới
        Task<ResultListDTO<WalletTransactionResponseDTO>> GetWalletTransactionByWalletId(string walletId, int page, int limit, CancellationToken cancellationToken = default); //  Lấy giao dịch ví với phân trang
        Task<ResultDTO> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default); // Xử lý giao dịch ví
    }
}
