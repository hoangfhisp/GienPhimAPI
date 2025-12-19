using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidTapPhimException : DomainException
    {
        public InvalidTapPhimException(string reason)
            : base($"Tập phim không hợp lệ: {reason}.") { }
    }
}