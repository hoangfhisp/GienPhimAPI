using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IBotConfigRepository
    {
        Task<BotConfig?> GetAsync(CancellationToken ct = default);
        Task<BotConfig> UpdateAsync(BotConfig entity, CancellationToken ct = default);
        Task<BotConfig> CreateAsync(BotConfig entity, CancellationToken ct = default);
    }
}
