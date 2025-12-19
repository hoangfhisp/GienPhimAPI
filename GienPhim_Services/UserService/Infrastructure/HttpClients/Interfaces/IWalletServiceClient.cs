using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.DTOs.Http;

namespace GienPhim_Services.UserService.Infrastructure.HttpClients.Interfaces
{
    public interface IWalletServiceClient
    {
        Task<ApiResponse<ResultDTO<WalletHttpResponseDTO>>> CreateWalletForUserAsync(CreateWalletHttpRequestDTO request, CancellationToken cancellationToken = default); // Tạo ví cho user mới
    }
}
