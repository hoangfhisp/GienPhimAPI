using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IFeedBackResponseMapper
    {
        FeedBackLikeDislikeResponseDTO toLikeDislikeDto(Feedback feedBack);
    }
}
