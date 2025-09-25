using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class DaoDienFailedException : BusinessException
    {
        public override int StatusCode => 422; // Unprocessable Entity

        public DaoDienFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Đạo diễn thất bại: {reason}.") { }
    }
}
