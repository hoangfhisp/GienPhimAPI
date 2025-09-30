using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateDienVienCuaPhimException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateDienVienCuaPhimException(int phimId, int dienVienId)
            : base($"Quan hệ PhimId={phimId}, DienVienId={dienVienId} đã tồn tại.") { }
    }
}
