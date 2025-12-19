using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdatePhimLikeDislikeFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public UpdatePhimLikeDislikeFailedException(int id)
            : base($"Cập nhật thông tin Like/Dislike cho phim {id} thất bại.")
        {
        }
    }
}
