using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class BotSyncLogRepository : IBotSyncLogRepository
    {
        private readonly FilmDbContext _context;

        public BotSyncLogRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BotSyncLog log, CancellationToken cancellationToken = default)
        {
            await _context.BotSyncLogs.AddAsync(log, cancellationToken);
        }

        public async Task<(List<BotSyncLog> logs, int total, int last)> GetAllAsync(
            int page, int limit, CancellationToken cancellationToken = default)
        {
            if (page <= 0) page = 1;
            if (limit <= 0) limit = 10;

            var query = _context.BotSyncLogs.AsNoTracking();

            var total = await query.CountAsync(cancellationToken);
            var last = total == 0 ? 0 : (int)Math.Ceiling((double)total / limit);

            var logs = await query
                .OrderByDescending(x => x.SyncedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (logs, total, last);
        }
    }
}
