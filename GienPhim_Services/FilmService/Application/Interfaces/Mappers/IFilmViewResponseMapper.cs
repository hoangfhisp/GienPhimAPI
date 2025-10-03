using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IFilmViewResponseMapper
    {
        FilmViewResponseDTO ToDto(FilmView filmView);
        List<FilmViewResponseDTO> ToDtoList(List<FilmView> filmViews);

        UpdateFilmViewResponseDTO ToUpdateDto(FilmView filmView);
        List<UpdateFilmViewResponseDTO> ToUpdateDtoList(List<FilmView> filmViews);
    }
}
