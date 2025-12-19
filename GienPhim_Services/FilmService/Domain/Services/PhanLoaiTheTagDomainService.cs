using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class PhanLoaiTheTagDomainService : IPhanLoaiTheTagDomainService
    {
        public void EnsureCanCreate(PhanLoaiTheTag phanLoaiTheTag, IEnumerable<PhanLoaiTheTag> existing)
        {
            if (phanLoaiTheTag == null)
                throw new InvalidPhanLoaiTheTagException("Entity không được null.");

            if (phanLoaiTheTag.PhimId <= 0)
                throw new InvalidPhanLoaiTheTagException("PhimId phải lớn hơn 0.");

            if (phanLoaiTheTag.TheTagId <= 0)
                throw new InvalidPhanLoaiTheTagException("TheTagId phải lớn hơn 0.");

            if (existing.Any(r => r.PhimId == phanLoaiTheTag.PhimId && r.TheTagId == phanLoaiTheTag.TheTagId))
                throw new DuplicatePhanLoaiTheTagException(phanLoaiTheTag.PhimId, phanLoaiTheTag.TheTagId);
        }

        public void EnsureCanUpdate(PhanLoaiTheTag phanLoaiTheTag, PhanLoaiTheTag? existing)
        {
            if (phanLoaiTheTag == null)
                throw new InvalidPhanLoaiTheTagException("Entity không được null.");

            if (existing == null)
                throw new PhanLoaiTheTagNotFoundException(phanLoaiTheTag.PhimId, phanLoaiTheTag.TheTagId);
        }

        public void EnsureCanDelete(PhanLoaiTheTag? existing)
        {
            if (existing == null)
                throw new PhanLoaiTheTagNotFoundException();
        }
    }
}
