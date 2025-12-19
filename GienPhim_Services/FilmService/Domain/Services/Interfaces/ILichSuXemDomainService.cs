using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface ILichSuXemDomainService
    {
        void EnsureCanCreate(LichSuXem lsx); // Kiểm tra các điều kiện để tạo Lịch Sử Xem mới
    }
}
