using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalids
{
    public class InvalidFilmViewException : DomainException
    {
        public InvalidFilmViewException(string reason)
            : base($"Dữ liệu FilmView không hợp lệ: {reason}") { }
    }
}