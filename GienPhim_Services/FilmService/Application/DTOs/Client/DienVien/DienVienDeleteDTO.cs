using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVien
{
    public class DienVienDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id diễn viên")]
        [Range(1, int.MaxValue, ErrorMessage = "Id diễn viên phải lớn hơn 0")]
        public int DienVienId { get; set; }
    }
}
