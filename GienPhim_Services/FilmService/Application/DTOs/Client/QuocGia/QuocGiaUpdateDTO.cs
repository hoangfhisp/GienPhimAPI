using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia
{
    public class QuocGiaUpdateDTO
    {
        [Required(ErrorMessage = "Thiếu Id quốc gia")]
        [Range(1, int.MaxValue, ErrorMessage = "Id quốc gia phải lớn hơn 0")]
        public int QuocGiaId { get; set; }

        [Required(ErrorMessage = "Tên quốc gia không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên quốc gia không được vượt quá {1} ký tự")]
        public string TenQG { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaQG { get; set; }
    }
}
