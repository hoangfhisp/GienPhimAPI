using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateTheLoaiFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateTheLoaiFailedException(string theLoaiName)
            : base($"Cập nhật Thể loại với tên {theLoaiName} thất bại.")
        {
        }
    }
}
