using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidFilmViewException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidFilmViewException(string reason)
            : base($"Dữ liệu FilmView không hợp lệ: {reason}") { }
    }
}