using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.Phim
{
    public class PhimDeleteDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PhimId { get; set; }
    }
}
