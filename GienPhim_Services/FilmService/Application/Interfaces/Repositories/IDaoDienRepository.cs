using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDaoDienRepository
    {
        Task<(List<DaoDien> daoDiens, int total, int last)> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<DaoDien?> GetByIdAsync(int daoDienId, CancellationToken ct = default);
        Task<DaoDien> CreateAsync(DaoDien daoDien, CancellationToken ct = default);
        Task<DaoDien> UpdateAsync(DaoDien daoDien, CancellationToken ct = default);
        Task DeleteAsync(DaoDien daoDien, CancellationToken ct = default);
        Task<List<DaoDien>> SearchDaoDiensAsync(string keyword, CancellationToken cancellationToken = default);
    }
}
