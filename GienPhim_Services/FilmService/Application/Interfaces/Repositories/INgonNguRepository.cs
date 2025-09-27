using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface INgonNguRepository
    {
        Task<List<NgonNgu>> GetAllNgonNgusAsync(CancellationToken cancellationToken = default);
        Task<NgonNgu?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<NgonNgu> CreateAsync(NgonNgu ngonNgu, CancellationToken ct = default);
        Task<NgonNgu> UpdateAsync(NgonNgu ngonNgu, CancellationToken ct = default);
        Task DeleteAsync(NgonNgu ngonNgu, CancellationToken ct = default);
    }
}
