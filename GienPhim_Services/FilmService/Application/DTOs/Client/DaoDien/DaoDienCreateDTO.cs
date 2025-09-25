using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien
{
    public class DaoDienCreateDTO
    {
        [Required(ErrorMessage = "Tên đạo diễn không được để trống")]
        [MaxLength(200, ErrorMessage = "Tên đạo diễn không được vượt quá 200 ký tự")]
        public string TenDD { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? MoTaDD { get; set; }
    }
}
