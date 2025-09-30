using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateTheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateTheLoaiFailedException(string theLoaiName)
            : base($"Tạo Thể loại với tên {theLoaiName} thất bại.")
        {
        }
    }
}
