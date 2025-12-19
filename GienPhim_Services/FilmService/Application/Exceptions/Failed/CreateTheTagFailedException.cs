using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateTheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateTheTagFailedException(string theTagName)
            : base($"Tạo TheTag với tên {theTagName} thất bại.")
        {
        }
    }
}
