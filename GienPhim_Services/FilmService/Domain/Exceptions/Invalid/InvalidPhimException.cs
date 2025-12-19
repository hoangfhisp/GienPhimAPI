using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidPhimException : DomainException
    {
        public InvalidPhimException(string reason)
            : base($"Phim không hợp lệ: {reason}.") { }
    }
}
