using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.DTOs.User;

namespace GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        // Gọi Api
        Task<ApiResponse<ResultDTO<UserHttpResponseDTO>>?> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Tìm User theo userId
    }
}
