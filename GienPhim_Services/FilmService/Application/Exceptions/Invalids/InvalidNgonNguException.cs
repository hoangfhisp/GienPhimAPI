using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidNgonNguException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidNgonNguException(string reason)
            : base($"Dữ liệu ngôn ngữ không hợp lệ: {reason}") { }
    }
}
