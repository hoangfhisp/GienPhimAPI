using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdatePhanLoaiTheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdatePhanLoaiTheLoaiFailedException(int phimId, int theLoaiId)
            : base($"Cập nhật quan hệ PhimId={phimId}, TheLoaiId={theLoaiId} thất bại.") { }
    }
}
