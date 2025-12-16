using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface ILichSuXemResponseMapper
    {
        LichSuXemResponseDTO toDTO(LichSuXem lichSuXem, Phim phim);
    }
}
