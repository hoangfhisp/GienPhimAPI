using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class BotConfig
    {
        [Key]
        public Guid BotConfigId { get; set; }

        [Required]
        [Range(1, 100)]
        public int ApiPerRun { get; set; } = 1; // Số lượng API mỗi lần quét

        [Required]
        [Range(1, 24)]
        public int IntervalHours { get; set; } = 1; // Số giờ giữa mỗi lần chạy

        [Required]
        public bool IsEnabled { get; set; } = true; // Bật / tắt bot

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
