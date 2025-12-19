using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateTapPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateTapPhimFailedException(string tenTap)
            : base($"Cập nhật Tập phim '{tenTap}' thất bại.") { }
    }
}
