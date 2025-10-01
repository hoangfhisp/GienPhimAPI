using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdatePhanLoaiTheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdatePhanLoaiTheTagFailedException(int phimId, int theTagId)
            : base($"Cập nhật quan hệ PhanLoaiTheTag (PhimId={phimId}, TheTagId={theTagId}) thất bại.") { }
    }
}
