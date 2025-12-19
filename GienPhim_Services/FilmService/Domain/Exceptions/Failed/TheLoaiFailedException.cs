using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class TheLoaiFailedException : DomainException
    {
        public TheLoaiFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Thể loại thất bại: {reason}.") { }
    }
}
