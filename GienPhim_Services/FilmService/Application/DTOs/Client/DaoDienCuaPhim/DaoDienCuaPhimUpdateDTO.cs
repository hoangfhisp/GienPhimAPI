using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim
{
    public class DaoDienCuaPhimUpdateDTO
    {
        [Required]
        public int PhimId { get; set; }

        [Required]
        public int DaoDienId { get; set; }

        [Required, MaxLength(DtoInvariants.VaiTroMaxLength)]
        public string VaiTroDG { get; set; } = string.Empty;
    }
}
