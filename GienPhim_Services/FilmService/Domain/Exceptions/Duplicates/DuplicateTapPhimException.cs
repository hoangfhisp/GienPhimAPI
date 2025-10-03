using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateTapPhimException : DomainException
    {
        public DuplicateTapPhimException(int phimId, string slugTap)
            : base($"Tập phim với Slug '{slugTap}' trong Phim Id={phimId} đã tồn tại.") { }
    }
}