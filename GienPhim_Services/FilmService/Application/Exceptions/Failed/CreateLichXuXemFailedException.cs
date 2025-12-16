using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateLichXuXemFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public CreateLichXuXemFailedException(int phimId)
            : base($"Tạo Lịch Xứ Xem cho PhimId={phimId} thất bại!") { }
    }
}
