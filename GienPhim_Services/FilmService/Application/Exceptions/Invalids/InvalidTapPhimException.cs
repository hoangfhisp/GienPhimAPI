using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidTapPhimException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidTapPhimException(string reason)
            : base($"Dữ liệu tập phim không hợp lệ: {reason}") { }
    }
}
