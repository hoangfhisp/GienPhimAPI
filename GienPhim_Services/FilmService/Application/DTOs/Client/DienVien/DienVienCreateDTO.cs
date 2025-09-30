using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVien
{
    public class DienVienCreateDTO
    {
        [Required(ErrorMessage = "Tên diễn viên không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên diễn viên không được vượt quá {1} ký tự")]
        public string TenDV { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaDV { get; set; }
    }
}
