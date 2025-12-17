using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface ITheLoaiRepository
    {
        Task<(List<TheLoai> theLoais, int total, int last)> GetAllTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<TheLoai?> GetByIdAsync(int theLoaiId, CancellationToken ct = default);
        Task<TheLoai> CreateAsync(TheLoai theLoai, CancellationToken ct = default);
        Task<TheLoai> UpdateAsync(TheLoai theLoai, CancellationToken ct = default);
        Task DeleteAsync(TheLoai theLoai, CancellationToken ct = default);
        Task<List<TheLoai>> SearchTheLoaisAsync(string keyword, CancellationToken cancellationToken = default);
        Task<TheLoai?> GetByMoTaAsync(string moTa, CancellationToken ct = default);
        Task<List<TheLoai>> GetByMoTasAsync(List<string> moTas, CancellationToken ct = default);
    }
}
