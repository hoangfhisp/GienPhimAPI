using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IFilmDomainService
    {
        Task<List<DaoDien>> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default); // Lấy danh sách tất cả đạo diễn
    }
}
