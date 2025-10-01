using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class PhanLoaiTheLoaiNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public PhanLoaiTheLoaiNotFoundException()
            : base("Không tìm thấy quan hệ Phim - Thể loại nào.") { }

        public PhanLoaiTheLoaiNotFoundException(int phimId, int theLoaiId)
            : base($"Không tìm thấy quan hệ với PhimId={phimId}, TheLoaiId={theLoaiId}.") { }
    }
}
