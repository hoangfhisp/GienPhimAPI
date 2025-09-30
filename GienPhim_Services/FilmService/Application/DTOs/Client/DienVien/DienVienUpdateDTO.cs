using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVien
{
    public class DienVienUpdateDTO
    {
        [Required(ErrorMessage = "Thiếu Id diễn viên")]
        [Range(1, int.MaxValue, ErrorMessage = "Id diễn viên phải lớn hơn 0")]
        public int DienVienId { get; set; }

        [Required(ErrorMessage = "Tên diễn viên không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên diễn viên không được vượt quá {1} ký tự")]
        public string TenDV { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaDV { get; set; }
    }
}
