using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class PhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public PhimFailedException(string action, string reason)
            : base($"Không thể {action} phim: {reason}") { }
    }
}
