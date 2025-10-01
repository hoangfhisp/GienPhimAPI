using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDaoDienCuaPhimResponseMapper
    {
        DaoDienCuaPhimResponseDTO ToDto(DaoDienCuaPhim daoDienCuaPhim);
        List<DaoDienCuaPhimResponseDTO> ToDtoList(List<DaoDienCuaPhim> daoDienCuaPhims);

        UpdateDaoDienCuaPhimResponseDTO ToUpdateDto(DaoDienCuaPhim daoDienCuaPhim);
        List<UpdateDaoDienCuaPhimResponseDTO> ToUpdateDtoList(List<DaoDienCuaPhim> daoDienCuaPhims);
    }
}
