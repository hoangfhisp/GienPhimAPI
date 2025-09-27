using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateNgonNguFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateNgonNguFailedException(string ngonNguName)
            : base($"Tạo NgônNgữ với tên {ngonNguName} thất bại.")
        {
        }
    }
}
