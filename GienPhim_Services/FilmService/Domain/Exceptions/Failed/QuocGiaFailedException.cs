using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class QuocGiaFailedException : DomainException
    {
        public QuocGiaFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Quốc gia thất bại: {reason}.") { }
    }
}
