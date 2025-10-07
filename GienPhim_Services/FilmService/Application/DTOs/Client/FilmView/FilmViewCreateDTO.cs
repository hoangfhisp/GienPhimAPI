using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.FilmView
{
    public class FilmViewCreateDTO
    {
        [Required]
        public int PhimId { get; set; }

        public string? UserId { get; set; }
    }
}
