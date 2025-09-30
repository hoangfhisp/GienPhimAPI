using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class DaoDienCuaPhimNotFoundException : DomainException
    {
        public DaoDienCuaPhimNotFoundException(int phimId, int daoDienId)
            : base($"Không tìm thấy quan hệ Đạo diễn - Phim với PhimId = {phimId}, DaoDienId = {daoDienId}.") { }
    }
}
