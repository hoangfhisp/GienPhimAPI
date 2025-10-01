using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicatePhanLoaiTheTagException : DomainException
    {
        public DuplicatePhanLoaiTheTagException(int phimId, int theTagId)
            : base($"Quan hệ PhimId={phimId} với TheTagId={theTagId} đã tồn tại.") { }
    }
}
