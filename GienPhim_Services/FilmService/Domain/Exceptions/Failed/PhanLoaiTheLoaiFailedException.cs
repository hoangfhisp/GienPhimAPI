using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class PhanLoaiTheLoaiFailedException : DomainException
    {
        public PhanLoaiTheLoaiFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với PhanLoaiTheLoai thất bại: {reason}.") { }
    }
}
