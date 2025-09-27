using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateQuocGiaFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateQuocGiaFailedException(string quocGiaName)
            : base($"Cập nhật Quốc gia với tên {quocGiaName} thất bại.")
        {
        }
    }
}
