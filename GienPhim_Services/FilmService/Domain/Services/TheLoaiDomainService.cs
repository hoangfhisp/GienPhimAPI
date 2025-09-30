using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class TheLoaiDomainService : ITheLoaiDomainService
    {
        public void EnsureCanCreate(TheLoai theLoai)
        {
            ValidateName(theLoai?.TenTL);
        }

        public void EnsureCanUpdate(TheLoai theLoai)
        {
            if (theLoai is null) throw new TheLoaiNotFoundException(0);
            ValidateName(theLoai.TenTL);
        }

        public void EnsureCanDelete(TheLoai theLoai)
        {
            if (theLoai is null) throw new TheLoaiNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidTheLoaiException("Tên không được để trống");

            if (v.Length > 200)
                throw new InvalidTheLoaiException("Tên vượt quá 200 ký tự");
        }
    }
}
