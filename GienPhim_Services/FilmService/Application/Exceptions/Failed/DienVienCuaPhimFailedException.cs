using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class DienVienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public DienVienCuaPhimFailedException(string action, string reason)
            : base($"Không thể {action} quan hệ Diễn viên - Phim: {reason}") { }
    }
}
