using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateDaoDienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateDaoDienFailedException(string daoDienName)
            : base($"Cập nhật DaoDien với tên {daoDienName} thất bại.")
        {
        }
    }
}
