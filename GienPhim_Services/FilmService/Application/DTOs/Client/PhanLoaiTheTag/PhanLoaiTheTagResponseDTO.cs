namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag
{
    public class PhanLoaiTheTagResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int TheTagId { get; set; }
        public string TenTheTag { get; set; } = string.Empty;
    }
}
