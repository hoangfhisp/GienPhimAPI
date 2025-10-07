using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidPhanLoaiTheLoaiException : DomainException
    {
        public InvalidPhanLoaiTheLoaiException(string reason)
            : base($"PhanLoaiTheLoai không hợp lệ: {reason}.") { }
    }
}
