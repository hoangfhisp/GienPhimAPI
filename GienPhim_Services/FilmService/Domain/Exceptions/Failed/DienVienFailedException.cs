using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Failed
{
    public class DienVienFailedException : DomainException
    {
        public DienVienFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với Diễn viên thất bại: {reason}.") { }
    }
}
