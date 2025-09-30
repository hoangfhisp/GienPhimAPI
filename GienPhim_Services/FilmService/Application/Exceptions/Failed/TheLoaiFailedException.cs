using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class TheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public TheLoaiFailedException(string action, string reason)
            : base($"Không thể {action} thể loại: {reason}") { }
    }
}
