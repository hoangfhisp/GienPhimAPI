using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim
{
    public class DienVienCuaPhimCreateDTO
    {
        [Required]
        public int PhimId { get; set; }

        [Required]
        public int DienVienId { get; set; }
    }
}
