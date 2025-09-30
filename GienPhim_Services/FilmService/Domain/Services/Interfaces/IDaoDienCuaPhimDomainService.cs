using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDaoDienCuaPhimDomainService
    {
        void EnsureCanCreate(DaoDienCuaPhim daoDienCuaPhim, IEnumerable<DaoDienCuaPhim> existingRelations);
        void EnsureCanUpdate(DaoDienCuaPhim daoDienCuaPhim, DaoDienCuaPhim? existingRelation);
        void EnsureCanDelete(DaoDienCuaPhim? existingRelation);
    }
}
