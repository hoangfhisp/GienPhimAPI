using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class FilmViewNotFoundException : DomainException
    {
        public FilmViewNotFoundException()
            : base("Không tìm thấy bản ghi FilmView") { }

        public FilmViewNotFoundException(string filmViewId)
            : base($"Không tìm thấy FilmView với Id = {filmViewId}") { }
    }
}