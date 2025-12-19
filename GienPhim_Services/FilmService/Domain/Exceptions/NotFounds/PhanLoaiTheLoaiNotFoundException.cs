using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class PhanLoaiTheLoaiNotFoundException : DomainException
    {
        public PhanLoaiTheLoaiNotFoundException(int phimId, int theLoaiId)
            : base($"Không tìm thấy quan hệ PhimId={phimId}, TheLoaiId={theLoaiId}.") { }
    }
}
