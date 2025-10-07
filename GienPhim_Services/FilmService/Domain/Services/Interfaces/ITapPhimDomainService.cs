using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface ITapPhimDomainService
    {
        void EnsureCanCreate(TapPhim tapPhim, IEnumerable<TapPhim> existing);
        void EnsureCanUpdate(TapPhim tapPhim, TapPhim? existing);
        void EnsureCanDelete(TapPhim? existing);
    }
}
