using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai
{
    public class TheLoaiDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id thể loại")]
        [Range(1, int.MaxValue, ErrorMessage = "Id thể loại phải lớn hơn 0")]
        public int TheLoaiId { get; set; }
    }
}
