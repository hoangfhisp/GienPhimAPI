using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class NgonNguDomainService : INgonNguDomainService
    {
        public void EnsureCanCreate(NgonNgu ngonNgu)
        {
            ValidateName(ngonNgu?.TenNN);
        }

        public void EnsureCanUpdate(NgonNgu ngonNgu)
        {
            if (ngonNgu is null) throw new NgonNguNotFoundException(0);
            ValidateName(ngonNgu.TenNN);
        }

        public void EnsureCanDelete(NgonNgu ngonNgu)
        {
            if (ngonNgu is null) throw new NgonNguNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidNgonNguException("Tên ngôn ngữ không được để trống");
            if (v.Length > 200)
                throw new InvalidNgonNguException("Tên ngôn ngữ vượt quá 200 ký tự");
        }
    }
}
