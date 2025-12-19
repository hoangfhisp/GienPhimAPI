using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class BotConfigNotFoundException : BusinessException
    {
        public BotConfigNotFoundException()
            : base("Không tìm thấy cấu hình bot trong hệ thống.") { }
    }
}
