using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class TheTagNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public TheTagNotFoundException(string? theTag = "")
            : base($"Thẻ tag {theTag} không tồn tại") { }
    }
}
