using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu
{
    public class NgonNguDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id ngôn ngữ")]
        [Range(1, int.MaxValue, ErrorMessage = "Id ngôn ngữ phải lớn hơn 0")]
        public int NgonNguId { get; set; }
    }
}
