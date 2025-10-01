using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class PhanLoaiTheTagNotFoundException : DomainException
    {
        public PhanLoaiTheTagNotFoundException()
            : base("Không tìm thấy quan hệ PhanLoaiTheTag nào.") { }

        public PhanLoaiTheTagNotFoundException(int phimId, int theTagId)
            : base($"Không tìm thấy quan hệ PhanLoaiTheTag với PhimId={phimId}, TheTagId={theTagId}.") { }
    }
}
