using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.FilmView
{
    public class FilmViewUpdateDTO
    {
        [Required]
        public string FilmViewId { get; set; } = string.Empty;

        [Required]
        public int PhimId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public DateTime ThoiGianXem { get; set; } // Cập nhật timestamp mới
    }
}
