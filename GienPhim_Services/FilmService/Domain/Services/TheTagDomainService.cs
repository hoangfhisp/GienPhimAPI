using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class TheTagDomainService : ITheTagDomainService
    {
        public void EnsureCanCreate(TheTag theTag)
        {
            ValidateName(theTag?.TenT);
        }

        public void EnsureCanUpdate(TheTag theTag)
        {
            if (theTag is null) throw new TheTagNotFoundException(0);
            ValidateName(theTag.TenT);
        }

        public void EnsureCanDelete(TheTag theTag)
        {
            if (theTag is null) throw new TheTagNotFoundException(0);
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidTheTagException("Tên không được để trống");
            if (v.Length > 200)
                throw new InvalidTheTagException("Tên vượt quá 200 ký tự");
        }
    }
}
