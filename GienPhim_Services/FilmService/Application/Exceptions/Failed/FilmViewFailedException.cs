using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class FilmViewFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public FilmViewFailedException(string action, string reason)
            : base($"Không thể {action} FilmView: {reason}") { }
    }
}