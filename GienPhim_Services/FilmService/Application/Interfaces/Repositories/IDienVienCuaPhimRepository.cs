using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDienVienCuaPhimRepository
    {
        Task<(List<DienVienCuaPhim> relations, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<DienVienCuaPhim?> GetByIdAsync(int phimId, int dienVienId, CancellationToken cancellationToken = default);
        Task<DienVienCuaPhim> CreateAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default);
        Task<DienVienCuaPhim> UpdateAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default);
    }
}
