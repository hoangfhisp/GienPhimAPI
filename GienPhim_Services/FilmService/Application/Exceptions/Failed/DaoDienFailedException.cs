using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class DaoDienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public DaoDienFailedException(string action, string reason)
            : base($"Không thể {action} đạo diễn: {reason}") { }
    }
}
