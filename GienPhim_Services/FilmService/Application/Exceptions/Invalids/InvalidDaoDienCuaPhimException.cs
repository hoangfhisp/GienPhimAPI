using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidDaoDienCuaPhimException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidDaoDienCuaPhimException(string reason)
            : base($"Dữ liệu quan hệ Đạo diễn - Phim không hợp lệ: {reason}") { }
    }
}
