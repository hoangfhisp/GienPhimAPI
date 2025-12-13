using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IBotConfigMapper
    {
        UpdateBotConfigResponseDTO ToResponseDto(BotConfig entity);
    }
}
