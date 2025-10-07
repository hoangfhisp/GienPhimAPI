using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IDienVienCuaPhimFactory
    {
        DienVienCuaPhim Create(DienVienCuaPhimCreateDTO dto);
        DienVienCuaPhim Update(DienVienCuaPhimUpdateDTO dto);
        DienVienCuaPhim Delete(DienVienCuaPhimDeleteDTO dto);
    }
}
