namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag
{
    public class UpdatePhanLoaiTheTagResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int TheTagId { get; set; }
        public string TenTheTag { get; set; } = string.Empty;

        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
