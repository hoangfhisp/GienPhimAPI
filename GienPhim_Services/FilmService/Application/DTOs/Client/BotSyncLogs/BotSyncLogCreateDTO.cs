using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.BotSyncLogs
{
    public class BotSyncLogCreateDTO
    {
        public string PhimName { get; set; } = string.Empty;
        public string PhimSlug { get; set; } = string.Empty;
        public BotSyncAction Action { get; set; }
        public string? Message { get; set; }
        public DateTime? SyncedAt { get; set; }
    }
}
