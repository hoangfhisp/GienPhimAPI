using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IPhimRepository
    {
        Task<(List<Phim> phims, int total, int last)> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<Phim?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Phim?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default);
        Task<Phim?> GetBySlugAsync(string slug, CancellationToken ct = default);
        Task<Phim> CreateAsync(Phim phim, CancellationToken cancellationToken = default);
        Task<Phim> UpdateAsync(Phim phim, CancellationToken cancellationToken = default);
        Task DeleteAsync(Phim phim, CancellationToken cancellationToken = default);
        Task<List<Phim>> SearchPhimsAsync(string keyword, CancellationToken cancellationToken = default);
        Task<(List<Phim> phims, int total, int last)> FilterPhimsAsync(int? ngonNguId, int? quocGiaId, int? dienVienId, int? daoDienId, int page, int limit, CancellationToken cancellationToken = default);
    }
}
