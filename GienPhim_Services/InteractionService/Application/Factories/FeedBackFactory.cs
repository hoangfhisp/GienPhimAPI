using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Factories
{
    public class FeedBackFactory : IFeedBackFactory
    {
        public Feedback CreateLikeDislike(ProcessingFeedBackLikeDislikeRequestDTO dto)
        {
            var newFeedback = new Feedback
            {
                FeedbackId = Guid.NewGuid().ToString("N"),
                PhimId = dto.PhimId,
                UserId = dto.UserId,
                LuotThichP = dto.LuotThichP,
                LuotDislikeP = dto.LuotDislikeP,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
            return newFeedback;
        }
    }
}
