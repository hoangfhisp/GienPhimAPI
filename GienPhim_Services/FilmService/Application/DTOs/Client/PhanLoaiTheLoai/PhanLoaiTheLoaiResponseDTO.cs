namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai
{
    public class PhanLoaiTheLoaiResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int TheLoaiId { get; set; }
        public string TenTheLoai { get; set; } = string.Empty;
    }
}
