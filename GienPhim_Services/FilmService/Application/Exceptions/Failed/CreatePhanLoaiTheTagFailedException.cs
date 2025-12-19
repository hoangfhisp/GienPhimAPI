using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreatePhanLoaiTheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreatePhanLoaiTheTagFailedException(int phimId, int theTagId)
            : base($"Tạo quan hệ PhanLoaiTheTag (PhimId={phimId}, TheTagId={theTagId}) thất bại.") { }
    }
}
