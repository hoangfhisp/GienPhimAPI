using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDienVienCuaPhimDomainService
    {
        void EnsureCanCreate(DienVienCuaPhim dienVienCuaPhim, IEnumerable<DienVienCuaPhim> existing);
        void EnsureCanUpdate(DienVienCuaPhim dienVienCuaPhim, DienVienCuaPhim? existing);
        void EnsureCanDelete(DienVienCuaPhim? existing);
    }
}
