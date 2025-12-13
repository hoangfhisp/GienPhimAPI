using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IBotConfigService
    {
        Task<ResultDTO<UpdateBotConfigResponseDTO>> GetBotConfigAsync(CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateBotConfigResponseDTO>> UpdateBotConfigAsync(BotConfigUpdateDTO dto, CancellationToken cancellationToken = default);
    }
}
