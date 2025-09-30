using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class DienVienCuaPhimDomainService : IDienVienCuaPhimDomainService
    {
        public void EnsureCanCreate(DienVienCuaPhim entity, IEnumerable<DienVienCuaPhim> existingRelations)
        {
            if (entity == null)
                throw new InvalidDienVienCuaPhimException("Entity không được null.");

            if (entity.PhimId <= 0)
                throw new InvalidDienVienCuaPhimException("PhimId phải lớn hơn 0.");

            if (entity.DienVienId <= 0)
                throw new InvalidDienVienCuaPhimException("DienVienId phải lớn hơn 0.");

            if (existingRelations.Any(r => r.PhimId == entity.PhimId && r.DienVienId == entity.DienVienId))
                throw new DuplicateDienVienCuaPhimException(entity.PhimId, entity.DienVienId);
        }

        public void EnsureCanUpdate(DienVienCuaPhim entity, DienVienCuaPhim? existingRelation)
        {
            if (entity == null)
                throw new InvalidDienVienCuaPhimException("Entity không được null.");

            if (existingRelation == null)
                throw new DienVienCuaPhimNotFoundException(entity.PhimId, entity.DienVienId);
        }

        public void EnsureCanDelete(DienVienCuaPhim? existingRelation)
        {
            if (existingRelation == null)
                throw new DienVienCuaPhimNotFoundException(0, 0);
        }
    }
}
