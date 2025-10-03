using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IFilmViewRepository
    {
        Task<(List<FilmView> filmViews, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<FilmView?> GetByIdAsync(string filmViewId, CancellationToken cancellationToken = default);
        Task<List<FilmView>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default);
        Task<FilmView?> GetLastViewByUserAsync(string? userId, int phimId, CancellationToken ct = default);
        Task<int> CountByPhimIdAsync(int phimId, CancellationToken ct = default);
        Task<FilmView> CreateAsync(FilmView filmView, CancellationToken cancellationToken = default);
        Task<FilmView> UpdateAsync(FilmView filmView, CancellationToken cancellationToken = default);
        Task DeleteAsync(FilmView filmView, CancellationToken cancellationToken = default);
    }
}
