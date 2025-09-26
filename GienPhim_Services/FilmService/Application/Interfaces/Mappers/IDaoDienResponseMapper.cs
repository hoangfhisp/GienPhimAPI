using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IDaoDienResponseMapper
    {
        DaoDienResponseDTO toDto(DaoDien daoDien); // Chuyển đổi từ DaoDien entity sang DaoDienResponseDTO
        List<DaoDienResponseDTO> toDtoList(List<DaoDien> daoDiens);// Chuyển đổi từ danh sách DaoDien entity sang danh sách DaoDienResponseDTO
        UpdateDaoDienResponseDTO ToUpdateDto(DaoDien daoDien);
        List<UpdateDaoDienResponseDTO> ToUpdateDtoList(List<DaoDien> daoDiens);
    }
}
