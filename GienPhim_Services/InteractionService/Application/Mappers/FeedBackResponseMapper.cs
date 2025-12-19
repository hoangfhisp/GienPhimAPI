using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class FeedBackResponseMapper : IFeedBackResponseMapper
    {
        public FeedBackLikeDislikeResponseDTO toLikeDislikeDto(Feedback feedBack)
        {
            return new FeedBackLikeDislikeResponseDTO
            {
                FeedbackId = feedBack.FeedbackId,
                PhimId = feedBack.PhimId,
                UserId = feedBack.UserId,
                LuotThichP = feedBack.LuotThichP,
                LuotDislikeP = feedBack.LuotDislikeP,
                ThoiGianTao = feedBack.ThoiGianTao,
                ThoiGianCapNhat = feedBack.ThoiGianCapNhat
            };
        }
    }
}
