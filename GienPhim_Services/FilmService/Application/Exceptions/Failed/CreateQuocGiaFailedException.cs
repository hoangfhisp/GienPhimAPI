using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateQuocGiaFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateQuocGiaFailedException(string quocGiaName)
            : base($"Tạo Quốc gia với tên {quocGiaName} thất bại.")
        {
        }
    }
}
