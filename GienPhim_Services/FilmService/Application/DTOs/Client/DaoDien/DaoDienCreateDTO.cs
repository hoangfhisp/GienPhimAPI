using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien
{
    public class DaoDienCreateDTO
    {
        [Required(ErrorMessage = "Tên đạo diễn không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên đạo diễn không được vượt quá {1} ký tự")]
        public string TenDD { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaDD { get; set; }
    }
}
