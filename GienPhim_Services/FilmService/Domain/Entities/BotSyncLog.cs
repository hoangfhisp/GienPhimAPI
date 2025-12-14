using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public enum BotSyncAction
    {
        Import = 1,
        Update = 2,
        Skip = 3,
        Error = 4
    }

    public class BotSyncLog
    {
        [Key]
        public Guid BotSyncLogId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(300)]
        public string PhimName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string PhimSlug { get; set; } = string.Empty;

        [Required]
        public BotSyncAction Action { get; set; }

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(2000)]
        public string? Message { get; set; }
    }
}