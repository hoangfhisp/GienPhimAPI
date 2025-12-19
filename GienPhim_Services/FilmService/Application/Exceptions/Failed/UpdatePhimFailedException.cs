using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdatePhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdatePhimFailedException(string phimName)
            : base($"Cập nhật Phim với tên {phimName} thất bại.") { }
    }
}
