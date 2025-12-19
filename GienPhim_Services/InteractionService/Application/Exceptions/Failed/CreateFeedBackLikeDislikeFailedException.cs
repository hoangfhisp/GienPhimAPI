using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Failed
{
    public class CreateFeedBackLikeDislikeFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public CreateFeedBackLikeDislikeFailedException()
            : base("Tạo mới like/dislike thất bại.")
        {
        }
    }
}
