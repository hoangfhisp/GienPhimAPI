using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public sealed class DaoDienFailedException : BusinessException
    {
        public override int StatusCode => 400;

        public DaoDienFailedException(string action, string reason)
            : base($"Không thể {action} đạo diễn: {reason}") { }
    }
}
