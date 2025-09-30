using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDaoDienCuaPhimRepository
    {
        Task<(List<DaoDienCuaPhim> relations, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim?> GetByIdAsync(int phimId, int daoDienId, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim> CreateAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim> UpdateAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default);
    }
}
