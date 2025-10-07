using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IPhanLoaiTheLoaiDomainService
    {
        void EnsureCanCreate(PhanLoaiTheLoai entity, IEnumerable<PhanLoaiTheLoai> existingRelations);
        void EnsureCanUpdate(PhanLoaiTheLoai entity, PhanLoaiTheLoai? existingRelation);
        void EnsureCanDelete(PhanLoaiTheLoai? existingRelation);
    }
}
