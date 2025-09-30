using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDienVienCuaPhimResponseMapper
    {
        DienVienCuaPhimResponseDTO ToDto(DienVienCuaPhim entity);
        List<DienVienCuaPhimResponseDTO> ToDtoList(List<DienVienCuaPhim> entities);

        UpdateDienVienCuaPhimResponseDTO ToUpdateDto(DienVienCuaPhim entity);
        List<UpdateDienVienCuaPhimResponseDTO> ToUpdateDtoList(List<DienVienCuaPhim> entities);
    }
}
