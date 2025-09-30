using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IPhimOrchestrator
    {
        Task<Phim> CreatePhimAsync(PhimCreateDTO createDto, string avatar, string poster, CancellationToken cancellationToken = default);
        Task<Phim> UpdatePhimAsync(PhimUpdateDTO updateDto, string? avatar, string? poster, CancellationToken cancellationToken = default);
        Task DeletePhimAsync(PhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
