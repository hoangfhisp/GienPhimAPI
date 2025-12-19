using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia
{
    public class QuocGiaDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id quốc gia")]
        [Range(1, int.MaxValue, ErrorMessage = "Id quốc gia phải lớn hơn 0")]
        public int QuocGiaId { get; set; }
    }
}
