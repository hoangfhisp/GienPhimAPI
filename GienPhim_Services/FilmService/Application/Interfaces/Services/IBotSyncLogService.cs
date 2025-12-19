using GienPhim_Services.FilmService.Application.DTOs.Client.BotSyncLogs;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IBotSyncLogService
    {
        Task CreateAsync(BotSyncLogCreateDTO dto, CancellationToken cancellationToken = default);

        Task<ResultListDTO<List<BotSyncLogResponseDTO>>> GetAllAsync(
            int page, int limit, CancellationToken cancellationToken = default);
    }
}
