using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class PhanLoaiTheTagFailedException : DomainException
    {
        public PhanLoaiTheTagFailedException(string action, string reason)
            : base($"Không thể {action} quan hệ PhanLoaiTheTag: {reason}") { }
    }
}
