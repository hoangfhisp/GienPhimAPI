using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicatePhanLoaiTheLoaiException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicatePhanLoaiTheLoaiException(int phimId, int theLoaiId)
            : base($"Quan hệ PhimId={phimId}, TheLoaiId={theLoaiId} đã tồn tại.") { }
    }
}
