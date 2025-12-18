using GienPhim_Services.InteractionService.Application.DTOs.Client.Http;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Phim;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IPhimServiceClient
    {
        Task<ApiResponse<ResultDTO<PhimHttpResponseDTO>>> FindPhimByPhimIdAsync(int phimId, CancellationToken cancellationToken = default);
        Task<ApiResponse<ResultDTO>?> UpdatePhimLikeDisLikeAsync(UpdatePhimLikeDisLikeRequestDTO request, CancellationToken cancellationToken = default);
    }
}
