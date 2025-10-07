using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateDaoDienCuaPhimException : DomainException
    {
        public DuplicateDaoDienCuaPhimException(int phimId, int daoDienId)
            : base($"Đạo diễn với Id = {daoDienId} đã được gán cho phim có Id = {phimId}.") { }
    }
}
