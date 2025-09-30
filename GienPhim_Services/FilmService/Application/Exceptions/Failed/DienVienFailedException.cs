using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class DienVienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public DienVienFailedException(string action, string reason)
            : base($"Không thể {action} diễn viên: {reason}") { }
    }
}
