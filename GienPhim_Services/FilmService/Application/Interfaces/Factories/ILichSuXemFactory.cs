using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface ILichSuXemFactory
    {
        LichSuXem Create(LichSuXemCreateDTO dto, string tapPhimId); // Tạo lịch sử xem mới
    }
}
