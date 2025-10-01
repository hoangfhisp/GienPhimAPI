using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai
{
    public class PhanLoaiTheLoaiDeleteDTO
    {
        [Required]
        public int PhimId { get; set; }

        [Required]
        public int TheLoaiId { get; set; }
    }
}
