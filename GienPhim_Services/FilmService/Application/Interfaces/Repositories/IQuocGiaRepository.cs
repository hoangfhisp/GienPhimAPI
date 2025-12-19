using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IQuocGiaRepository
    {
        Task<(List<QuocGia> quocGias, int total, int last)> GetAllQuocGiasAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<QuocGia?> GetByIdAsync(int quocGiaId, CancellationToken ct = default);
        Task<QuocGia> CreateAsync(QuocGia quocGia, CancellationToken ct = default);
        Task<QuocGia> UpdateAsync(QuocGia quocGia, CancellationToken ct = default);
        Task DeleteAsync(QuocGia quocGia, CancellationToken ct = default);
        Task<List<QuocGia>> SearchQuocGiasAsync(string keyword, CancellationToken cancellationToken = default);
        Task<QuocGia?> GetByMoTaQGAsync(string moTaQG, CancellationToken ct = default);
        Task<List<QuocGia>> GetByMoTaQGsAsync(List<string> moTaQGs, CancellationToken ct = default);
    }
}
