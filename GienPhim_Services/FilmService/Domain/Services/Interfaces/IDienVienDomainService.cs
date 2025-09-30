using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDienVienDomainService
    {
        void EnsureCanCreate(DienVien dienVien);
        void EnsureCanUpdate(DienVien dienVien);
        void EnsureCanDelete(DienVien dienVien);
    }
}
