using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class PhanLoaiTheTagNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public PhanLoaiTheTagNotFoundException()
            : base("Không tìm thấy quan hệ PhanLoaiTheTag nào.") { }

        public PhanLoaiTheTagNotFoundException(int phimId, int theTagId)
            : base($"Không tìm thấy quan hệ PhanLoaiTheTag với PhimId={phimId}, TheTagId={theTagId}.") { }
    }
}
