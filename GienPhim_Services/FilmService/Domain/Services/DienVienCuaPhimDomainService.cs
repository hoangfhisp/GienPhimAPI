using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class DienVienCuaPhimDomainService : IDienVienCuaPhimDomainService
    {
        public void EnsureCanCreate(DienVienCuaPhim dienVienCuaPhim, IEnumerable<DienVienCuaPhim> existing)
        {
            if (dienVienCuaPhim == null)
                throw new InvalidDienVienCuaPhimException("Entity không được null.");

            if (dienVienCuaPhim.PhimId <= 0)
                throw new InvalidDienVienCuaPhimException("PhimId phải lớn hơn 0.");

            if (dienVienCuaPhim.DienVienId <= 0)
                throw new InvalidDienVienCuaPhimException("DienVienId phải lớn hơn 0.");

            if (existing.Any(r => r.PhimId == dienVienCuaPhim.PhimId && r.DienVienId == dienVienCuaPhim.DienVienId))
                throw new DuplicateDienVienCuaPhimException(dienVienCuaPhim.PhimId, dienVienCuaPhim.DienVienId);
        }

        public void EnsureCanUpdate(DienVienCuaPhim dienVienCuaPhim, DienVienCuaPhim? existing)
        {
            if (dienVienCuaPhim == null)
                throw new InvalidDienVienCuaPhimException("Entity không được null.");

            if (existing == null)
                throw new DienVienCuaPhimNotFoundException(dienVienCuaPhim.PhimId, dienVienCuaPhim.DienVienId);
        }

        public void EnsureCanDelete(DienVienCuaPhim? existing)
        {
            if (existing == null)
                throw new DienVienCuaPhimNotFoundException(0, 0);
        }
    }
}
