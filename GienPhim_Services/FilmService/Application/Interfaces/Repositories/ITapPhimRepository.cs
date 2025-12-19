using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface ITapPhimRepository
    {
        Task<(List<TapPhim> tapPhims, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<TapPhim?> GetByIdAsync(string tapId, CancellationToken cancellationToken = default);
        Task<List<TapPhim>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default);
        Task<TapPhim> FindByTapSugAndPhimId(string tapSlug, int phimId, CancellationToken cancellationToken = default); // Tìm kiếm tập phim theo Slug và PhimId
        Task<TapPhim> CreateAsync(TapPhim tapPhim, CancellationToken cancellationToken = default);
        Task<TapPhim> UpdateAsync(TapPhim tapPhim, CancellationToken cancellationToken = default);
        Task DeleteAsync(TapPhim tapPhim, CancellationToken cancellationToken = default);
        Task<(List<TapPhim> tapPhims, int total, int last)> SearchTapPhimAsync(string keyword, int page, int limit, CancellationToken ct = default);
    }
}
