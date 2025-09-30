using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class DienVienDomainService : IDienVienDomainService
    {
        public void EnsureCanCreate(DienVien dienVien)
        {
            ValidateName(dienVien?.TenDV);
        }

        public void EnsureCanUpdate(DienVien dienVien)
        {
            if (dienVien is null) throw new DienVienNotFoundException(0);
            ValidateName(dienVien.TenDV);
        }

        public void EnsureCanDelete(DienVien dienVien)
        {
            if (dienVien is null) throw new DienVienNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidDienVienException("Tên không được để trống");
            if (v.Length > 200)
                throw new InvalidDienVienException("Tên vượt quá 200 ký tự");
        }
    }
}
