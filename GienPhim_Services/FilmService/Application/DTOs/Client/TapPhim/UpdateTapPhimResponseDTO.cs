namespace GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim
{
    public class UpdateTapPhimResponseDTO
    {
        public string TapId { get; set; } = string.Empty;
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;

        public string TenTap { get; set; } = string.Empty;
        public string SlugTap { get; set; } = string.Empty;
        public string TenFile { get; set; } = string.Empty;
        public string? LinkChinh { get; set; }
        public string LinkPhu { get; set; } = string.Empty;

        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
