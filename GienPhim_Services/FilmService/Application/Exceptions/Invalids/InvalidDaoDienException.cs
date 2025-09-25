using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public sealed class InvalidDaoDienException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidDaoDienException(string reason)
            : base($"Dữ liệu đạo diễn không hợp lệ: {reason}") { }
    }
}
