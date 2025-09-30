using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface INgonNguResponseMapper
    {
        NgonNguResponseDTO ToDto(NgonNgu ngonNgu);
        List<NgonNguResponseDTO> ToDtoList(List<NgonNgu> ngonNgus);
        UpdateNgonNguResponseDTO ToUpdateDto(NgonNgu ngonNgu);
        List<UpdateNgonNguResponseDTO> ToUpdateDtoList(List<NgonNgu> ngonNgus);
    }
}
