namespace GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig
{
    public class UpdateBotConfigResponseDTO
    {
        public bool IsEnabled { get; set; }
        public int ApiPerRun { get; set; }
        public int IntervalHours { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
