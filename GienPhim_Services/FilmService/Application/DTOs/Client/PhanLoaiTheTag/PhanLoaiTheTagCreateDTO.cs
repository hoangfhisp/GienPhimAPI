using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag
{
    public class PhanLoaiTheTagCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PhimId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TheTagId { get; set; }
    }
}
