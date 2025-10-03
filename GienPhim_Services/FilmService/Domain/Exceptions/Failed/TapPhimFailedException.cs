using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class TapPhimFailedException : DomainException
    {
        public TapPhimFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với tập phim thất bại: {reason}.") { }
    }
}