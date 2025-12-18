using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Phim;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators
{
    public interface IFeedBackOrchestrator
    {
        Task<Feedback> ProcessingLikeDislikeAsync(ProcessingFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default); // xử lý like dislike
        Task<ResultDTO> UpdatePhimLikeDisLikeAsync(UpdatePhimLikeDisLikeRequestDTO request, CancellationToken cancellationToken = default); // Cập nhật like dislike phim
    }
}
