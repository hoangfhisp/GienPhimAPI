using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateDaoDienCuaPhimException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateDaoDienCuaPhimException(int phimId, int daoDienId)
            : base($"Quan hệ PhimId={phimId}, DaoDienId={daoDienId} đã tồn tại.") { }
    }
}
