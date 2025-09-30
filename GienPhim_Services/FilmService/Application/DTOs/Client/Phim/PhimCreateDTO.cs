using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.Phim
{
    public class PhimCreateDTO
    {
        [Required, MaxLength(DtoInvariants.TenMaxLength)]
        public string TenP { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaMaxLength)]
        public string? MoTaP { get; set; }

        [Required]
        public int QuocGiaId { get; set; }

        [Required]
        public int NgonNguId { get; set; }

        [Required, MaxLength(DtoInvariants.TenCTSXPMaxLength)]
        public string TenCTSXP { get; set; } = string.Empty;

        [Required]
        public DateTime NgayPhatHanhP { get; set; }

        [Required]
        public string ThuoiLuongP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Avatar phim bắt buộc")]
        public IFormFile AvatarP { get; set; }

        [Required(ErrorMessage = "Poster phim bắt buộc")]
        public IFormFile AvatarPoster { get; set; }

        [Required, MaxLength(DtoInvariants.NguonMaxLength)]
        public string NguonP { get; set; } = string.Empty;

        public string? SoTapP { get; set; }
        public string? TraiLerP { get; set; }

        // Quan hệ nhiều-nhiều
        public List<int>? TheLoaiIds { get; set; }
        public List<int>? DaoDienIds { get; set; }
        public List<int>? DienVienIds { get; set; }
        public List<int>? TheTagIds { get; set; }
    }
}
