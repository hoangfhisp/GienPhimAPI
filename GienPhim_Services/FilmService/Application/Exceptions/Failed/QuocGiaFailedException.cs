using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class QuocGiaFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public QuocGiaFailedException(string action, string reason)
            : base($"Không thể {action} quốc gia: {reason}") { }
    }
}
