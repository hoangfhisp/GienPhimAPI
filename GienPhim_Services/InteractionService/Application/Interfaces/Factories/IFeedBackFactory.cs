using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Factories
{
    public interface IFeedBackFactory
    {
        Feedback CreateLikeDislike(ProcessingFeedBackLikeDislikeRequestDTO dto);
        
    }
}
