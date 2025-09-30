using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface ITheLoaiDomainService
    {
        void EnsureCanCreate(TheLoai theLoai);
        void EnsureCanUpdate(TheLoai theLoai);
        void EnsureCanDelete(TheLoai theLoai);
    }
}
