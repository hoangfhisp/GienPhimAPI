using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim
{
    public class TapPhimDeleteDTO
    {
        [Required]
        public string TapId { get; set; } = string.Empty;
    }
}
