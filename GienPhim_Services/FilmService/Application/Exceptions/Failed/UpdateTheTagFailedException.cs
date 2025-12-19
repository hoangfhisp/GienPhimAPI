using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateTheTagFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateTheTagFailedException(string theTagName)
            : base($"Cập nhật TheTag với tên {theTagName} thất bại.")
        {
        }
    }
}
