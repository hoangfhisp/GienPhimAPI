using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IFilmViewService
    {
        Task<ResultDTO> CreateAsync(FilmViewCreateDTO dto, CancellationToken ct = default);
        Task<ResultDTO<UpdateFilmViewResponseDTO>> UpdateAsync(FilmViewUpdateDTO dto, CancellationToken ct = default);
        Task<ResultDTO> DeleteAsync(FilmViewDeleteDTO dto, CancellationToken ct = default);
    }
}
