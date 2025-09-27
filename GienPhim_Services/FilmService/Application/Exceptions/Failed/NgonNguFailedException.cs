using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class NgonNguFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public NgonNguFailedException(string action, string reason)
            : base($"Không thể {action} ngôn ngữ: {reason}") { }
    }
}
