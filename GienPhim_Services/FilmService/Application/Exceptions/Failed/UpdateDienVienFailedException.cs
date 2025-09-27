using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateDienVienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateDienVienFailedException(string dienVienName)
            : base($"Cập nhật Diễn Viên với tên {dienVienName} thất bại.") { }
    }
}
