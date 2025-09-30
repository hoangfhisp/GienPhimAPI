using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDaoDienCuaPhimResponseMapper
    {
        DaoDienCuaPhimResponseDTO ToDto(DaoDienCuaPhim entity);
        List<DaoDienCuaPhimResponseDTO> ToDtoList(List<DaoDienCuaPhim> entities);

        UpdateDaoDienCuaPhimResponseDTO ToUpdateDto(DaoDienCuaPhim entity);
        List<UpdateDaoDienCuaPhimResponseDTO> ToUpdateDtoList(List<DaoDienCuaPhim> entities);
    }
}
