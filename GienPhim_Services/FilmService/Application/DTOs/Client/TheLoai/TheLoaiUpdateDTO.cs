using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai
{
    public class TheLoaiUpdateDTO
    {
        [Required(ErrorMessage = "Thiếu Id thể loại")]
        [Range(1, int.MaxValue, ErrorMessage = "Id thể loại phải lớn hơn 0")]
        public int TheLoaiId { get; set; }

        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        [MaxLength(DtoInvariants.TenMaxLength, ErrorMessage = "Tên thể loại không được vượt quá {1} ký tự")]
        public string TenTL { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTa { get; set; }
    }
}
