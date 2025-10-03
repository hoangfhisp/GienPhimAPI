using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface ITapPhimOrchestrator
    {
        Task<TapPhim> CreateTapPhimAsync(TapPhimCreateDTO createDto, string? linkChinh, CancellationToken cancellationToken = default);
        Task<TapPhim> UpdateTapPhimAsync(TapPhimUpdateDTO updateDto, string? linkChinh, CancellationToken cancellationToken = default);
        Task DeleteTapPhimAsync(TapPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
