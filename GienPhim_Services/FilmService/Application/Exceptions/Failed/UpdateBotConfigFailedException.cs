using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateBotConfigFailedException : BusinessException
    {
        public UpdateBotConfigFailedException(string action, string reason)
            : base($"Thực hiện '{action}' thất bại: {reason}.") { }
    }
}
