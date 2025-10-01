using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDienVienCuaPhimResponseMapper
    {
        DienVienCuaPhimResponseDTO ToDto(DienVienCuaPhim dienVienCuaPhim);
        List<DienVienCuaPhimResponseDTO> ToDtoList(List<DienVienCuaPhim> dienVienCuaPhims);

        UpdateDienVienCuaPhimResponseDTO ToUpdateDto(DienVienCuaPhim dienVienCuaPhim);
        List<UpdateDienVienCuaPhimResponseDTO> ToUpdateDtoList(List<DienVienCuaPhim> dienVienCuaPhims);
    }
}
