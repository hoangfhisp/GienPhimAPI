using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class PhanLoaiTheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public PhanLoaiTheTagFailedException(string action, string reason)
            : base($"Không thể {action} quan hệ PhanLoaiTheTag: {reason}") { }
    }
}
