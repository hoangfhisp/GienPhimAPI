using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien
{
    public class DaoDienDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id đạo diễn")]
        [Range(1, int.MaxValue, ErrorMessage = "Id đạo diễn phải lớn hơn 0")]
        public int DaoDienId { get; set; }
    }
}
