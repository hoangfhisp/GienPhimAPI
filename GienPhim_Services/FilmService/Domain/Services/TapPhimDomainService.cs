using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class TapPhimDomainService : ITapPhimDomainService
    {
        public void EnsureCanCreate(TapPhim tapPhim, IEnumerable<TapPhim> existing)
        {
            if (tapPhim == null)
                throw new InvalidTapPhimException("Entity không được null.");

            if (tapPhim.PhimId <= 0)
                throw new InvalidTapPhimException("PhimId phải lớn hơn 0.");

            if (string.IsNullOrWhiteSpace(tapPhim.TenTap))
                throw new InvalidTapPhimException("Tên tập không được để trống.");

            if (string.IsNullOrWhiteSpace(tapPhim.SlugTap))
                throw new InvalidTapPhimException("Slug tập không được để trống.");

            if (string.IsNullOrWhiteSpace(tapPhim.LinkPhu))
                throw new InvalidTapPhimException("Link phụ không được để trống.");

            // Check duplicate Slug trong cùng 1 phim
            if (existing.Any(e => e.PhimId == tapPhim.PhimId && e.SlugTap == tapPhim.SlugTap))
                throw new DuplicateTapPhimException(tapPhim.PhimId, tapPhim.SlugTap);
        }

        public void EnsureCanUpdate(TapPhim tapPhim, TapPhim? existing)
        {
            if (tapPhim == null)
                throw new InvalidTapPhimException("Entity không được null.");

            if (existing == null)
                throw new TapPhimNotFoundException(tapPhim.TapId);

            if (string.IsNullOrWhiteSpace(tapPhim.TenTap))
                throw new InvalidTapPhimException("Tên tập không được để trống.");

            if (string.IsNullOrWhiteSpace(tapPhim.SlugTap))
                throw new InvalidTapPhimException("Slug tập không được để trống.");
        }

        public void EnsureCanDelete(TapPhim? existing)
        {
            if (existing == null)
                throw new TapPhimNotFoundException("Không xác định được TapId để xoá.");
        }
    }
}
