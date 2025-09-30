using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicatePhimException : DomainException
    {
        public DuplicatePhimException(string tenPhim)
            : base($"Phim '{tenPhim}' đã tồn tại.") { }
    }
}
