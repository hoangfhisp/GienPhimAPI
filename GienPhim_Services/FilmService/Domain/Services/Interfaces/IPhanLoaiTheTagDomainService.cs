using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IPhanLoaiTheTagDomainService
    {
        void EnsureCanCreate(PhanLoaiTheTag phanLoaiTheTag, IEnumerable<PhanLoaiTheTag> existing);
        void EnsureCanUpdate(PhanLoaiTheTag phanLoaiTheTag, PhanLoaiTheTag? existing);
        void EnsureCanDelete(PhanLoaiTheTag? existing);
    }
}
