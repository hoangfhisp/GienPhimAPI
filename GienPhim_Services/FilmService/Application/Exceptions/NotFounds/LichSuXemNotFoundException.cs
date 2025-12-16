using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class LichSuXemNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public LichSuXemNotFoundException(string? user = "")
            : base($"Lich Su Xem User {user} không tồn tại") { }
    }
}
