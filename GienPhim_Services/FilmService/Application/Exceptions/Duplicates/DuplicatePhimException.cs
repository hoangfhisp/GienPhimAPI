using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicatePhimException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicatePhimException(string tenPhim)
            : base($"Tên phim '{tenPhim}' đã tồn tại.") { }
    }
}