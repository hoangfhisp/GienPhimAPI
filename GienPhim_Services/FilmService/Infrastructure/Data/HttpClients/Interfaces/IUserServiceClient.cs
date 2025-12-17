using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.DTOs.User;

namespace GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        Task<ApiResponse<ResultDTO<UserResponseDTO>>?> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Tìm User theo userId
    }
}
