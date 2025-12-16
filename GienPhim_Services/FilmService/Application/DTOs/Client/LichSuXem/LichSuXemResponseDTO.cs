namespace GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem
{
    public class LichSuXemResponseDTO
    {
        public int LichSuXemId { get; set; }
        public int PhimId { get; set; }
        public string TenP { get; set; }
        public string MoTaP { get; set; }
        public string AvatarP { get; set; }
        public string AvatarPoster { get; set; }
        public string TenCTSXP { get; set; }
        public string ThuoiLuongP { get; set; }
        public string? SoTapP { get; set; }
        public long LuotXemP { get; set; }
        public long LuotDislikeP { get; set; }
        public long LuotThichP { get; set; }
        public string UserId { get; set; }
        public string TapPhimId { get; set; }
        public string TapVuaXem { get; set; }
        public DateTime ThoiGianXem { get; set; } = DateTime.Now;
    }
}
