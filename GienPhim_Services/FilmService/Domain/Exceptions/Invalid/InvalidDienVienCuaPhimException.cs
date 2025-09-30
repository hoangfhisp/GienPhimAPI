using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidDienVienCuaPhimException : DomainException
    {
        public InvalidDienVienCuaPhimException(string reason)
            : base($"Quan hệ Diễn viên - Phim không hợp lệ: {reason}") { }
    }
}
