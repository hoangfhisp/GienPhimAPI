using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IDienVienRepository
    {
        Task<List<DienVien>> GetAllDienViensAsync(CancellationToken cancellationToken = default);
        Task<DienVien?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<DienVien> CreateAsync(DienVien dienVien, CancellationToken ct = default);
        Task<DienVien> UpdateAsync(DienVien dienVien, CancellationToken ct = default);
        Task DeleteAsync(DienVien dienVien, CancellationToken ct = default);
    }
}
