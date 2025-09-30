using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim
{
    public class DaoDienCuaPhimDeleteDTO
    {
        [Required]
        public int PhimId { get; set; }

        [Required]
        public int DaoDienId { get; set; }
    }
}
