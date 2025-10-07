using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface ITapPhimResponseMapper
    {
        TapPhimResponseDTO ToDto(TapPhim tapPhim);
        List<TapPhimResponseDTO> ToDtoList(List<TapPhim> tapPhims);

        UpdateTapPhimResponseDTO ToUpdateDto(TapPhim tapPhim);
        List<UpdateTapPhimResponseDTO> ToUpdateDtoList(List<TapPhim> tapPhims);
    }
}
