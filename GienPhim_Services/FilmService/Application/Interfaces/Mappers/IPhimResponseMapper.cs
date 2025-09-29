using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IPhimResponseMapper
    {
        PhimResponseDTO ToDto(Phim phim);
        List<PhimResponseDTO> ToDtoList(List<Phim> phims);

        UpdatePhimResponseDTO ToUpdateDto(Phim phim);
        List<UpdatePhimResponseDTO> ToUpdateDtoList(List<Phim> phims);
    }
}
