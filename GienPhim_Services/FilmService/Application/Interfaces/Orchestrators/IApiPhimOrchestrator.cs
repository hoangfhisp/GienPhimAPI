using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IApiPhimOrchestrator
    {
        Task<Phim> ProcessImportAsync(string slug, CancellationToken ct = default);
        Task<Phim> ProcessUpdateAsync(string slug, CancellationToken ct = default);
    }
}
