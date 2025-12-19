using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateDienVienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateDienVienFailedException(string dienVienName)
            : base($"Tạo Diễn Viên với tên {dienVienName} thất bại.") { }
    }
}
