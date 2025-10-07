using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidPhanLoaiTheTagException : DomainException
    {
        public InvalidPhanLoaiTheTagException(string reason)
            : base($"Quan hệ PhanLoaiTheTag không hợp lệ: {reason}") { }
    }
}
