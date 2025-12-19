using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class PhanLoaiTheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public PhanLoaiTheLoaiFailedException(string action, string reason)
            : base($"Không thể {action} quan hệ Phim - Thể loại: {reason}") { }
    }
}
