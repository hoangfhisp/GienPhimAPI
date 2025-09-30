using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class QuocGiaDomainService : IQuocGiaDomainService
    {
        public void EnsureCanCreate(QuocGia quocGia)
        {
            ValidateName(quocGia?.TenQG);
        }

        public void EnsureCanUpdate(QuocGia quocGia)
        {
            if (quocGia is null) throw new QuocGiaNotFoundException(0);
            ValidateName(quocGia.TenQG);
        }

        public void EnsureCanDelete(QuocGia quocGia)
        {
            if (quocGia is null) throw new QuocGiaNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidQuocGiaException("Tên không được để trống");
            if (v.Length > 200)
                throw new InvalidQuocGiaException("Tên vượt quá 200 ký tự");
        }
    }
}
