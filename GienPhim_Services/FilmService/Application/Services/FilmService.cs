using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmOrchestrator _filmOrchestrator; // Đạo Diễn Repository

        public FilmService(IFilmOrchestrator filmOrchestrator)
        {
            _filmOrchestrator = filmOrchestrator;
        }

        // Dao Dien
        // Lấy danh sách tất cả đạo diễn
        public async Task<ResultListDTO<List<DaoDienResponseDTO>>> GetAllDaoDiensAsync(int page, int limit , CancellationToken cancellationToken = default)
        {
            return await _filmOrchestrator.GetAllDaoDiensAsync(page, limit, cancellationToken);
        }

    }
}
