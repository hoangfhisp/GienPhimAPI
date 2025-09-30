using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class DaoDienCuaPhimFailedException : DomainException
    {
        public DaoDienCuaPhimFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với quan hệ Đạo diễn - Phim thất bại: {reason}.") { }
    }
}
