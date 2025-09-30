using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IDienVienCuaPhimDomainService
    {
        void EnsureCanCreate(DienVienCuaPhim entity, IEnumerable<DienVienCuaPhim> existingRelations);
        void EnsureCanUpdate(DienVienCuaPhim entity, DienVienCuaPhim? existingRelation);
        void EnsureCanDelete(DienVienCuaPhim? existingRelation);
    }
}
