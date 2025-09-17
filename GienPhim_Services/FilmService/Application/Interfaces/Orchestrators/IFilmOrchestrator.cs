using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IFilmOrchestrator
    {
        Task<ResultListDTO<List<DaoDienResponseDTO>>> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default); // Lấy danh sách tất cả đạo diễn
    }
}
