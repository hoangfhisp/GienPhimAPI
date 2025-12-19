using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateTapPhimException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateTapPhimException(int phimId, string tenTap)
            : base($"Tập phim '{tenTap}' đã tồn tại trong PhimId={phimId}.") { }
    }
}
