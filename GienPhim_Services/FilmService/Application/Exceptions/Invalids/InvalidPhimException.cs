using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidPhimException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidPhimException(string reason)
            : base($"Dữ liệu phim không hợp lệ: {reason}") { }
    }
}