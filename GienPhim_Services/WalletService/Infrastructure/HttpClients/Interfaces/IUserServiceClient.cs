using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.User;

namespace GienPhim_Services.WalletService.Infrastructure.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        Task<ApiResponse<ResultDTO<UserHttpResponseDTO>>> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Tìm User theo id
    }
}
