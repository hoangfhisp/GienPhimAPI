using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Http;
using GienPhim_Services.MembershipService.Application.DTOs.Wallets;

namespace GienPhim_Services.MembershipService.Infrastructure.HttpClients.Interfaces
{
    public interface IWalletServiceClient
    {
        Task<ApiResponse<ResultDTO<WalletHttpResponseDTO>>> CreateWalletForUserAsync(CreateWalletHttpRequestDTO request, CancellationToken cancellationToken = default); // Tạo ví cho user mới
        Task<ApiResponse<ResultDTO<WalletHttpResponseDTO>>> GetWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default); // lấy thong tin ví theo userId
        Task<ApiResponse<ResultDTO>> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default); // Tạo giao dịch ví
    }
}
