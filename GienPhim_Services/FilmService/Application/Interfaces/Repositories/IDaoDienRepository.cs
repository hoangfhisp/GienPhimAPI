using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDaoDienRepository
    {
        Task<List<DaoDien>> GetAllDaoDiensAsync(CancellationToken cancellationToken = default);
        Task<DaoDien?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<DaoDien> CreateAsync(DaoDien daoDien, CancellationToken ct = default);
        Task<DaoDien> UpdateAsync(DaoDien daoDien, CancellationToken ct = default);
        Task DeleteAsync(DaoDien daoDien, CancellationToken ct = default);
    }
}
