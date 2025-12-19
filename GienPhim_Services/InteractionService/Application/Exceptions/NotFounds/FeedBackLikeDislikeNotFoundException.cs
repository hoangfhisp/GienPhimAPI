using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class FeedBackLikeDislikeNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public FeedBackLikeDislikeNotFoundException()
            : base("Feedback like/dislike not found.")
        {
        }
    }
}
