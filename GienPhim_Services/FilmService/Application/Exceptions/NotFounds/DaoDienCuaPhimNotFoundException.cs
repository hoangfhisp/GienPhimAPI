using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class DaoDienCuaPhimNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public DaoDienCuaPhimNotFoundException()
            : base("Không tìm thấy quan hệ Đạo diễn - Phim nào.") { }

        public DaoDienCuaPhimNotFoundException(int phimId, int daoDienId)
            : base($"Không tìm thấy quan hệ với PhimId={phimId}, DaoDienId={daoDienId}.") { }
    }
}
