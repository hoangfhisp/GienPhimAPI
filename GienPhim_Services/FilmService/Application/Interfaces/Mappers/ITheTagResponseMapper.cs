using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface ITheTagResponseMapper
    {
        TheTagResponseDTO toDto(TheTag theTag);
        List<TheTagResponseDTO> toDtoList(List<TheTag> theTags);
        UpdateTheTagResponseDTO ToUpdateDto(TheTag theTag);
        List<UpdateTheTagResponseDTO> ToUpdateDtoList(List<TheTag> theTags);
    }
}
