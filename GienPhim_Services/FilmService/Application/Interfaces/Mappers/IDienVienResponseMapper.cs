using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDienVienResponseMapper
    {
        DienVienResponseDTO toDto(DienVien dienVien);
        List<DienVienResponseDTO> toDtoList(List<DienVien> dienViens);
        UpdateDienVienResponseDTO ToUpdateDto(DienVien dienVien);
        List<UpdateDienVienResponseDTO> ToUpdateDtoList(List<DienVien> dienViens);
    }
}
