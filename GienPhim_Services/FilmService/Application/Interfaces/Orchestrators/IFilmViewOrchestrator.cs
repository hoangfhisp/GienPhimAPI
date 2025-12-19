using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IFilmViewOrchestrator
    {
        Task<FilmView> CreateAsync(FilmViewCreateDTO dto, CancellationToken cancellationToken = default);
        Task<FilmView> UpdateAsync(FilmViewUpdateDTO dto, FilmView? existing, CancellationToken cancellationToken = default);
        Task DeleteAsync(FilmViewDeleteDTO dto, FilmView? existing, CancellationToken cancellationToken = default);
    }
}
