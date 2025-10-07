using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IPhanLoaiTheLoaiRepository
    {
        Task<(List<PhanLoaiTheLoai> phanLoaiTheLoais, int total, int last)> GetAllPhanLoaiTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheLoai?> GetByIdAsync(int phimId, int theLoaiId, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheLoai> CreateAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheLoai> UpdateAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default);
        Task DeleteAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default);
    }
}
