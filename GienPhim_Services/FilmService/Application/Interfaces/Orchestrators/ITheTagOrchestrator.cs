using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface ITheTagOrchestrator
    {
        Task<TheTag> CreateTheTagAsync(TheTagCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<TheTag> UpdateTheTagAsync(TheTagUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteTheTagAsync(TheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
