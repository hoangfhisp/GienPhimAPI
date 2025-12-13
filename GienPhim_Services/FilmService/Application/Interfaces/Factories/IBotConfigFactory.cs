using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IBotConfigFactory
    {
        BotConfig CreateDefault();
        void Update(BotConfig entity, BotConfigUpdateDTO dto);
    }
}
