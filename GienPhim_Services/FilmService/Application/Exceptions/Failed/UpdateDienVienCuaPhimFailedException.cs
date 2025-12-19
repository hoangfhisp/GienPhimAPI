using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateDienVienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateDienVienCuaPhimFailedException(int phimId, int dienVienId)
            : base($"Cập nhật quan hệ PhimId={phimId}, DienVienId={dienVienId} thất bại.") { }
    }
}
