using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IBotConfigOrchestrator
    {
        Task<UpdateBotConfigResponseDTO> GetAsync(CancellationToken ct = default);
        Task<UpdateBotConfigResponseDTO> UpdateAsync(BotConfigUpdateDTO dto, CancellationToken ct = default);
    }
}
