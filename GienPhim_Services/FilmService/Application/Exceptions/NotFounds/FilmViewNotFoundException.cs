using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class FilmViewNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public FilmViewNotFoundException()
            : base("Không tìm thấy bản ghi FilmView nào") { }

        public FilmViewNotFoundException(string filmViewId)
            : base($"Không tìm thấy FilmView với Id={filmViewId}") { }
    }
}