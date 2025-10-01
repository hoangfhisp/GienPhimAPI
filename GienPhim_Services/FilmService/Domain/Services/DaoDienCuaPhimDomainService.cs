using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class DaoDienCuaPhimDomainService : IDaoDienCuaPhimDomainService
    {
        public void EnsureCanCreate(DaoDienCuaPhim daoDienCuaPhim, IEnumerable<DaoDienCuaPhim> existing)
        {
            if (daoDienCuaPhim == null)
                throw new InvalidDaoDienCuaPhimException("Entity không được null.");

            if (daoDienCuaPhim.PhimId <= 0)
                throw new InvalidDaoDienCuaPhimException("PhimId phải lớn hơn 0.");

            if (daoDienCuaPhim.DaoDienId <= 0)
                throw new InvalidDaoDienCuaPhimException("DaoDienId phải lớn hơn 0.");

            if (string.IsNullOrWhiteSpace(daoDienCuaPhim.VaiTroDG))
                throw new InvalidDaoDienCuaPhimException("Vai trò đạo diễn không được để trống.");

            // Kiểm tra duplicate
            if (existing.Any(r => r.PhimId == daoDienCuaPhim.PhimId && r.DaoDienId == daoDienCuaPhim.DaoDienId))
            {
                throw new DuplicateDaoDienCuaPhimException(daoDienCuaPhim.PhimId, daoDienCuaPhim.DaoDienId);
            }
        }

        public void EnsureCanUpdate(DaoDienCuaPhim daoDienCuaPhim, DaoDienCuaPhim? existing)
        {
            if (daoDienCuaPhim == null)
                throw new InvalidDaoDienCuaPhimException("Entity không được null.");

            if (existing == null)
                throw new DaoDienCuaPhimNotFoundException(daoDienCuaPhim.PhimId, daoDienCuaPhim.DaoDienId);

            if (string.IsNullOrWhiteSpace(daoDienCuaPhim.VaiTroDG))
                throw new InvalidDaoDienCuaPhimException("Vai trò đạo diễn không được để trống.");
        }

        public void EnsureCanDelete(DaoDienCuaPhim? existing)
        {
            if (existing == null)
                throw new DaoDienCuaPhimNotFoundException(0, 0);
            // TODO: Nếu ở service layer bạn có phimId/daoDienId thực tế thì truyền vào để log rõ ràng hơn
        }
    }
}
