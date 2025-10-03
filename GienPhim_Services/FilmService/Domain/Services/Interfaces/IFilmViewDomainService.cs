using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IFilmViewDomainService
    {
        void EnsureCanCreate(FilmView filmView);
        void EnsureCanUpdate(FilmView filmView, FilmView? existing);
        void EnsureCanDelete(FilmView? existing);
    }
}
