using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidTheLoaiException : DomainException
    {
        public InvalidTheLoaiException(string reason)
            : base($"Thể loại không hợp lệ: {reason}.") { }
    }
}
