using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class DienVienCuaPhimFailedException : DomainException
    {
        public DienVienCuaPhimFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với quan hệ Diễn viên - Phim thất bại: {reason}.") { }
    }
}
