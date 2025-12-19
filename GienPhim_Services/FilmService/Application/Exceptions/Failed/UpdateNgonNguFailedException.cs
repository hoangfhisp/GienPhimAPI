using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateNgonNguFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateNgonNguFailedException(string ngonNguName)
            : base($"Cập nhật NgônNgữ với tên {ngonNguName} thất bại.")
        {
        }
    }
}
