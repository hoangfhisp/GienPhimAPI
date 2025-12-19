using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateTapPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateTapPhimFailedException(string tenTap)
            : base($"Tạo Tập phim '{tenTap}' thất bại.") { }
    }
}
