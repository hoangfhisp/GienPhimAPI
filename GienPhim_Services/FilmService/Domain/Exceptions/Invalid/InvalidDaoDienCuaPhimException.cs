using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidDaoDienCuaPhimException : DomainException
    {
        public InvalidDaoDienCuaPhimException(string reason)
            : base($"Quan hệ Đạo diễn - Phim không hợp lệ: {reason}.") { }
    }
}
