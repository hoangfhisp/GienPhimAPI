using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IQuocGiaDomainService
    {
        void EnsureCanCreate(QuocGia quocGia);
        void EnsureCanUpdate(QuocGia quocGia);
        void EnsureCanDelete(QuocGia quocGia);
    }
}
