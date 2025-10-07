using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreatePhanLoaiTheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreatePhanLoaiTheLoaiFailedException(int phimId, int theLoaiId)
            : base($"Tạo quan hệ PhimId={phimId}, TheLoaiId={theLoaiId} thất bại.") { }
    }
}
