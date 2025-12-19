namespace GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai
{
    public class UpdatePhanLoaiTheLoaiResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int TheLoaiId { get; set; }
        public string TenTheLoai { get; set; } = string.Empty;

        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
