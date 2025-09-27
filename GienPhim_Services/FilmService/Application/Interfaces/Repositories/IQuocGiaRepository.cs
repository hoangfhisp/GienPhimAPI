using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IQuocGiaRepository
    {
        Task<List<QuocGia>> GetAllQuocGiasAsync(CancellationToken cancellationToken = default);
        Task<QuocGia?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<QuocGia> CreateAsync(QuocGia quocGia, CancellationToken ct = default);
        Task<QuocGia> UpdateAsync(QuocGia quocGia, CancellationToken ct = default);
        Task DeleteAsync(QuocGia quocGia, CancellationToken ct = default);
    }
}
