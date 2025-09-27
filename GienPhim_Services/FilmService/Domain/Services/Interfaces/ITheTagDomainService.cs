using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface ITheTagDomainService
    {
        void EnsureCanCreate(TheTag theTag);
        void EnsureCanUpdate(TheTag theTag);
        void EnsureCanDelete(TheTag theTag);
    }
}
