using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class DaoDienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public DaoDienCuaPhimFailedException(string action, string reason)
            : base($"Không thể {action} quan hệ Đạo diễn - Phim: {reason}") { }
    }
}
