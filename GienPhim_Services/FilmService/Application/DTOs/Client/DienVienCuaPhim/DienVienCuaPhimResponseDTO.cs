namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim
{
    public class DienVienCuaPhimResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int DienVienId { get; set; }
        public string TenDienVien { get; set; } = string.Empty;

        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
