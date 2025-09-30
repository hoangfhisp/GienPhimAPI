using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateDaoDienFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateDaoDienFailedException(string daoDienName)
            : base($"Tạo DaoDien với tên {daoDienName} thất bại.")
        {
        }
    }
}
