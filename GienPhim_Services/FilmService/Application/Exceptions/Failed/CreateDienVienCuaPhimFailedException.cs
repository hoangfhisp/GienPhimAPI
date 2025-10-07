using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateDienVienCuaPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateDienVienCuaPhimFailedException(int phimId, int dienVienId)
            : base($"Tạo quan hệ PhimId={phimId}, DienVienId={dienVienId} thất bại.") { }
    }
}
