using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class DaoDienDomainService : IDaoDienDomainService
    {
        public void EnsureCanCreate(DaoDien daoDien)
        {
            ValidateName(daoDien?.TenDD);
        }

        public void EnsureCanUpdate(DaoDien daoDien)
        {
            if (daoDien is null) throw new DaoDienNotFoundException(0);
            ValidateName(daoDien.TenDD);
        }

        public void EnsureCanDelete(DaoDien daoDien)
        {
            if (daoDien is null) throw new DaoDienNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidDaoDienException("Tên không được để trống");
            if (v.Length > 200)
                throw new InvalidDaoDienException("Tên vượt quá 200 ký tự");
        }
    }
}
