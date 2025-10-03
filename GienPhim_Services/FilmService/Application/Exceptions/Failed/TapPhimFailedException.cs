using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class TapPhimFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public TapPhimFailedException(string action, string reason)
            : base($"Không thể {action} tập phim: {reason}") { }
    }
}
