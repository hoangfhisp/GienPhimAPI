using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicatePhanLoaiTheLoaiException : DomainException
    {
        public DuplicatePhanLoaiTheLoaiException(int phimId, int theLoaiId)
            : base($"Quan hệ PhimId={phimId} và TheLoaiId={theLoaiId} đã tồn tại.") { }
    }
}
