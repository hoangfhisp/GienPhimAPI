using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidTheTagException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidTheTagException(string reason)
            : base($"Dữ liệu thẻ tag không hợp lệ: {reason}") { }
    }
}
