using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Failed
{
    public class UpdateFeedBackLikeDislikeFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public UpdateFeedBackLikeDislikeFailedException() : base("Cập nhật like, dislike thất bại")
        {
        }
    }
}
