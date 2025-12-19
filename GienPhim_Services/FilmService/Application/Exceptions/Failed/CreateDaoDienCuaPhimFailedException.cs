using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateDaoDienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateDaoDienCuaPhimFailedException(int phimId, int daoDienId)
            : base($"Tạo quan hệ PhimId={phimId}, DaoDienId={daoDienId} thất bại.") { }
    }
}
