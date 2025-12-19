using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class DaoDienFailedException : DomainException
    {
        public DaoDienFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Đạo diễn thất bại: {reason}.") { }
    }
}
