using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IPhanLoaiTheTagRepository
    {
        Task<(List<PhanLoaiTheTag> phanLoaiTheTags, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheTag?> GetByIdAsync(int phimId, int theTagId, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheTag> CreateAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheTag> UpdateAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default);
        Task DeleteAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default);
    }
}
