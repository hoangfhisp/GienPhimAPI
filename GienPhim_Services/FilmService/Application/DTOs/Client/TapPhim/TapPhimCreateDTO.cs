using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim
{
    public class TapPhimCreateDTO
    {
        [Required]
        public int PhimId { get; set; }

        [Required, MaxLength(DtoInvariants.TenTapMaxLength)]
        public string TenTap { get; set; } = string.Empty;

        [Required, MaxLength(DtoInvariants.SlugTapMaxLength)]
        public string SlugTap { get; set; } = string.Empty;

        [Required, MaxLength(DtoInvariants.TenFileMaxLength)]
        public string TenFile { get; set; } = string.Empty;

        public IFormFile? LinkChinh { get; set; }

        [Required, MaxLength(DtoInvariants.LinkMaxLength)]
        public string LinkPhu { get; set; } = string.Empty;
    }
}