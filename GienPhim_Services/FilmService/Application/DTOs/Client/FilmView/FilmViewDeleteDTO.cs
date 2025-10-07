using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.FilmView
{
    public class FilmViewDeleteDTO
    {
        [Required]
        public string FilmViewId { get; set; } = string.Empty;
    }
}
