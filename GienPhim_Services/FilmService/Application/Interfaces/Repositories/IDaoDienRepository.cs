using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDaoDienRepository
    {
        Task<List<DaoDien>> GetAllDaoDiensAsync(CancellationToken cancellationToken = default);
    }
}
