using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu
{
    public class NgonNguUpdateDTO
    {
        [Required(ErrorMessage = "Thiếu Id ngôn ngữ")]
        [Range(1, int.MaxValue, ErrorMessage = "Id ngôn ngữ phải lớn hơn 0")]
        public int NgonNguId { get; set; }

        [Required(ErrorMessage = "Tên ngôn ngữ không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên ngôn ngữ không được vượt quá {1} ký tự")]
        public string TenNN { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaNN { get; set; }
    }
}
