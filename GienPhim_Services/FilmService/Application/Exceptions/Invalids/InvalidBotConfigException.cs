using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidBotConfigException : BusinessException
    {
        public InvalidBotConfigException(string reason)
            : base($"Cấu hình Bot không hợp lệ: {reason}.") { }
    }
}
