using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDaoDienCuaPhimDomainService
    {
        void EnsureCanCreate(DaoDienCuaPhim daoDienCuaPhim, IEnumerable<DaoDienCuaPhim> existing);
        void EnsureCanUpdate(DaoDienCuaPhim daoDienCuaPhim, DaoDienCuaPhim? existing);
        void EnsureCanDelete(DaoDienCuaPhim? existing);
    }
}
