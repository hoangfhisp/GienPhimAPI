using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidDienVienCuaPhimException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidDienVienCuaPhimException(string reason)
            : base($"Dữ liệu quan hệ Diễn viên - Phim không hợp lệ: {reason}") { }
    }
}
