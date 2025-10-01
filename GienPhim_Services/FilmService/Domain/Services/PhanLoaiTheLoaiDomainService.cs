using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class PhanLoaiTheLoaiDomainService : IPhanLoaiTheLoaiDomainService
    {
        public void EnsureCanCreate(PhanLoaiTheLoai entity, IEnumerable<PhanLoaiTheLoai> existingRelations)
        {
            if (entity == null)
                throw new InvalidPhanLoaiTheLoaiException("Entity không được null.");

            if (entity.PhimId <= 0)
                throw new InvalidPhanLoaiTheLoaiException("PhimId phải lớn hơn 0.");

            if (entity.TheLoaiId <= 0)
                throw new InvalidPhanLoaiTheLoaiException("TheLoaiId phải lớn hơn 0.");

            // Check duplicate (1 phim không thể có cùng 1 thể loại 2 lần)
            if (existingRelations.Any(r => r.PhimId == entity.PhimId && r.TheLoaiId == entity.TheLoaiId))
                throw new DuplicatePhanLoaiTheLoaiException(entity.PhimId, entity.TheLoaiId);
        }

        public void EnsureCanUpdate(PhanLoaiTheLoai entity, PhanLoaiTheLoai? existingRelation)
        {
            if (entity == null)
                throw new InvalidPhanLoaiTheLoaiException("Entity không được null.");

            if (existingRelation == null)
                throw new PhanLoaiTheLoaiNotFoundException(entity.PhimId, entity.TheLoaiId);

            if (entity.PhimId <= 0)
                throw new InvalidPhanLoaiTheLoaiException("PhimId phải lớn hơn 0.");

            if (entity.TheLoaiId <= 0)
                throw new InvalidPhanLoaiTheLoaiException("TheLoaiId phải lớn hơn 0.");
        }

        public void EnsureCanDelete(PhanLoaiTheLoai? existingRelation)
        {
            if (existingRelation == null)
                throw new PhanLoaiTheLoaiNotFoundException(0, 0);
            // có thể truyền Id thực tế nếu muốn rõ ràng hơn
        }
    }
}
