using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateDaoDienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateDaoDienCuaPhimFailedException(int phimId, int daoDienId)
            : base($"Cập nhật quan hệ PhimId={phimId}, DaoDienId={daoDienId} thất bại.") { }
    }
}
