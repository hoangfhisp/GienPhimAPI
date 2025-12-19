using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class TheTagFailedException : DomainException
    {
        public TheTagFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Thẻ tag thất bại: {reason}.") { }
    }
}
