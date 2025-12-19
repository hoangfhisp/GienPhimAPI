using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDaoDienCuaPhimRepository
    {
        Task<(List<DaoDienCuaPhim> daoDienCuaPhims, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim?> GetByIdAsync(int phimId, int daoDienId, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim> CreateAsync(DaoDienCuaPhim daoDienCuaPhim, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim> UpdateAsync(DaoDienCuaPhim daoDienCuaPhim, CancellationToken cancellationToken = default);
        Task DeleteAsync(DaoDienCuaPhim daoDienCuaPhim, CancellationToken cancellationToken = default);
    }
}
