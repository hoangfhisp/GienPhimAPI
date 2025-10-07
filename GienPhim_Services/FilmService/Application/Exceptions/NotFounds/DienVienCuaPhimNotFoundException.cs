using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class DienVienCuaPhimNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public DienVienCuaPhimNotFoundException()
            : base("Không tìm thấy quan hệ Diễn viên - Phim nào.") { }

        public DienVienCuaPhimNotFoundException(int phimId, int dienVienId)
            : base($"Không tìm thấy quan hệ với PhimId={phimId}, DienVienId={dienVienId}.") { }
    }
}
