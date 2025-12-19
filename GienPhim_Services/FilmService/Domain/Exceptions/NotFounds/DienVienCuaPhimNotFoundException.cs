using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class DienVienCuaPhimNotFoundException : DomainException
    {
        public DienVienCuaPhimNotFoundException(int phimId, int dienVienId)
            : base($"Không tìm thấy quan hệ Diễn viên - Phim với PhimId={phimId}, DienVienId={dienVienId}.") { }
    }
}
