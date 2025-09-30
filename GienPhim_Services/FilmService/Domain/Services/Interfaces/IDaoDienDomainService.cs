using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDaoDienDomainService
    {
        void EnsureCanCreate(DaoDien daoDien);
        void EnsureCanUpdate(DaoDien daoDien);
        void EnsureCanDelete(DaoDien daoDien);
    }
}
