using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreatePhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreatePhimFailedException(string phimName)
            : base($"Tạo Phim với tên {phimName} thất bại.") { }
    }
}
