using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheTag
{
    public class TheTagDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id thẻ tag")]
        [Range(1, int.MaxValue, ErrorMessage = "Id thẻ tag phải lớn hơn 0")]
        public int TheTagId { get; set; }
    }
}
