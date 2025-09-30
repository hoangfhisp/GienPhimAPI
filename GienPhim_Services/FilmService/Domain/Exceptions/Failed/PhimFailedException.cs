using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class PhimFailedException : DomainException
    {
        public PhimFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với phim thất bại: {reason}.") { }
    }
}
