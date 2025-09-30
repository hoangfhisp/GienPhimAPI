using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface INgonNguDomainService
    {
        void EnsureCanCreate(NgonNgu ngonNgu);
        void EnsureCanUpdate(NgonNgu ngonNgu);
        void EnsureCanDelete(NgonNgu ngonNgu);
    }
}
