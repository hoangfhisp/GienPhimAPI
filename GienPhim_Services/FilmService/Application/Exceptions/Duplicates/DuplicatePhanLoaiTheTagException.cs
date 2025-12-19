using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicatePhanLoaiTheTagException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicatePhanLoaiTheTagException(int phimId, int theTagId)
            : base($"Quan hệ PhimId={phimId} với TheTagId={theTagId} đã tồn tại.") { }
    }
}
