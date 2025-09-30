namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim
{
    public class UpdateDaoDienCuaPhimResponseDTO
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public int DaoDienId { get; set; }
        public string TenDaoDien { get; set; } = string.Empty;

        public string VaiTroDG { get; set; } = string.Empty;

        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
