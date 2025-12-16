using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IPhimRepository
    {
        Task<(List<Phim> phims, int total, int last)> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<Phim?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Phim?> GetBySlugAsync(string slug, CancellationToken ct = default);
        Task<Phim> CreateAsync(Phim phim, CancellationToken cancellationToken = default);
        Task<Phim> UpdateAsync(Phim phim, CancellationToken cancellationToken = default);
        Task<(List<Phim> phims, int total, int last)> GetNewPhimAsync(int page, int limit, CancellationToken cancellationToken = default); // Lấy danh sách new phim
        Task<(List<Phim> phims, int total, int last)> GetTopViewPhimAsync(int page, int limit, CancellationToken cancellationToken = default); // Lấy danh sách top view phim
        Task DeleteAsync(Phim phim, CancellationToken cancellationToken = default);
        Task<(List<Phim> phims, int total, int last)> SearchPhimsAsync(string keyword, int page, int limit, CancellationToken cancellationToken = default); // Tìm kiếm phim theo từ khóa (gần đúng)
        Task<(List<Phim> phims, int total, int last)> FilterPhimsAsync(string? ngonNguSlug, string? quocGiaSlug, string? theLoaiSlug, string? tenDienVien, string? tenDaoDien, int page, int limit, CancellationToken cancellationToken = default);

    }
}
