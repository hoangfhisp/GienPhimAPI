using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IDaoDienCuaPhimFactory
    {
        DaoDienCuaPhim Create(DaoDienCuaPhimCreateDTO dto);
        DaoDienCuaPhim Update(DaoDienCuaPhimUpdateDTO dto);
        DaoDienCuaPhim Delete(DaoDienCuaPhimDeleteDTO dto);
    }
}
