using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class NgonNguNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public NgonNguNotFoundException(string? ngonNgu = "")
            : base($"Ngôn Ngữ {ngonNgu} không tồn tại") { }
    }
}
