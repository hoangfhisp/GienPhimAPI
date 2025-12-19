using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateDienVienCuaPhimException : DomainException
    {
        public DuplicateDienVienCuaPhimException(int phimId, int dienVienId)
            : base($"Diễn viên với Id={dienVienId} đã được gán cho phim Id={phimId}.") { }
    }
}
