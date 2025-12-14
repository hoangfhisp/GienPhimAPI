using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IBotSyncLogRepository
    {
        Task AddAsync(BotSyncLog log, CancellationToken cancellationToken = default);

        Task<(List<BotSyncLog> logs, int total, int last)> GetAllAsync(
            int page, int limit, CancellationToken cancellationToken = default);
    }
}
