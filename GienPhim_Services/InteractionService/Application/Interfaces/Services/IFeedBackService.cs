using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IFeedBackService
    {
        Task<ResultDTO<FeedBackLikeDislikeResponseDTO>> GetLikeDislikeByUserIdAndPhimIdAsync(GetFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default); // Lấy Like Dislike
        Task<ResultDTO> ProcessingLikeDislikeAsync(ProcessingFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default); // Xử lý Like Dislike
    }
}
