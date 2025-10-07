using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalids;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class FilmViewDomainService : IFilmViewDomainService
    {
        public void EnsureCanCreate(FilmView filmView)
        {
            if (filmView == null)
                throw new InvalidFilmViewException("Entity FilmView không được null.");

            if (filmView.PhimId <= 0)
                throw new InvalidFilmViewException("PhimId phải lớn hơn 0.");

            // UserId có thể null (xem ẩn danh), nhưng nếu có thì phải hợp lệ
            if (filmView.UserId != null && string.IsNullOrWhiteSpace(filmView.UserId))
                throw new InvalidFilmViewException("UserId không hợp lệ.");
        }

        public void EnsureCanUpdate(FilmView filmView, FilmView? existing)
        {
            if (filmView == null)
                throw new InvalidFilmViewException("Entity FilmView không được null.");

            if (existing == null)
                throw new FilmViewNotFoundException(filmView.FilmViewId);
        }

        public void EnsureCanDelete(FilmView? existing)
        {
            if (existing == null)
                throw new FilmViewNotFoundException();
        }
    }
}
