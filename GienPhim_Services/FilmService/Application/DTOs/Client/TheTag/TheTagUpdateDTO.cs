using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheTag
{
    public class TheTagUpdateDTO
    {
        [Required(ErrorMessage = "Thiếu Id thẻ tag")]
        [Range(1, int.MaxValue, ErrorMessage = "Id thẻ tag phải lớn hơn 0")]
        public int TheTagId { get; set; }

        [Required(ErrorMessage = "Tên thẻ tag không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên thẻ tag không được vượt quá {1} ký tự")]
        public string TenT { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaT { get; set; }
    }
}
