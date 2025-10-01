using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IPhanLoaiTheLoaiFactory
    {
        PhanLoaiTheLoai Create(PhanLoaiTheLoaiCreateDTO dto);
        PhanLoaiTheLoai Update(PhanLoaiTheLoaiUpdateDTO dto);
        PhanLoaiTheLoai Delete(PhanLoaiTheLoaiDeleteDTO dto);
    }
}
