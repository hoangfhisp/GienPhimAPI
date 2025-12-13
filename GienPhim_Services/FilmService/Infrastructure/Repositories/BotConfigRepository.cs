using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class BotConfigRepository : IBotConfigRepository
    {
        private readonly FilmDbContext _context;

        public BotConfigRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<BotConfig?> GetAsync(CancellationToken ct = default)
        {
            return await _context.BotConfigs.FirstOrDefaultAsync(ct);
        }

        public async Task<BotConfig> CreateAsync(BotConfig entity, CancellationToken ct = default)
        {
            _context.BotConfigs.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<BotConfig> UpdateAsync(BotConfig entity, CancellationToken ct = default)
        {
            _context.BotConfigs.Update(entity);
            await _context.SaveChangesAsync(ct);
            return entity;
        }
    }
}
