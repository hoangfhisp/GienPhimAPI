using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class NgonNguFailedException : DomainException
    {
        public NgonNguFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Ngôn ngữ thất bại: {reason}.") { }
    }
}
