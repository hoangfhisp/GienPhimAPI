using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class TheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public TheTagFailedException(string action, string reason)
            : base($"Không thể {action} thẻ tag: {reason}") { }
    }
}
