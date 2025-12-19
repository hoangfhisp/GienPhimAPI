using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateLichSuXemFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public UpdateLichSuXemFailedException(string userId, int phimId)
            : base($"Cập nhật Lịch Sử Xem cho UserId={userId}, PhimId={phimId} thất bại!") { }
    }
}
