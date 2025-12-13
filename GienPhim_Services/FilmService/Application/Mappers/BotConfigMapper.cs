using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class BotConfigMapper : IBotConfigMapper
    {
        public UpdateBotConfigResponseDTO ToResponseDto(BotConfig entity)
        {
            return new UpdateBotConfigResponseDTO
            {
                IsEnabled = entity.IsEnabled,
                ApiPerRun = entity.ApiPerRun,
                IntervalHours = entity.IntervalHours,
                LastUpdated = entity.LastUpdated
            };
        }
    }
}
