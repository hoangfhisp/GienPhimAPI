using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class BotConfigFactory : IBotConfigFactory
    {
        public BotConfig CreateDefault()
        {
            return new BotConfig
            {
                IsEnabled = false,
                ApiPerRun = 20,
                IntervalHours = 3,
                LastUpdated = DateTime.UtcNow
            };
        }

        public void Update(BotConfig entity, BotConfigUpdateDTO dto)
        {
            if (dto.ApiPerRun <= 0 || dto.ApiPerRun > 100)
                throw new InvalidBotConfigException("Giá trị ApiPerRun phải nằm trong khoảng 1–100.");

            if (dto.IntervalHours <= 0 || dto.IntervalHours > 24)
                throw new InvalidBotConfigException("Giá trị IntervalHours phải nằm trong khoảng 1–24.");

            entity.IsEnabled = dto.IsEnabled;
            entity.ApiPerRun = dto.ApiPerRun;
            entity.IntervalHours = dto.IntervalHours;
            entity.LastUpdated = DateTime.UtcNow;
        }
    }
}
