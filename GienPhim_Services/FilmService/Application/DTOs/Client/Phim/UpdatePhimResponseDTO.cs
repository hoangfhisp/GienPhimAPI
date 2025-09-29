namespace GienPhim_Services.FilmService.Application.DTOs.Client.Phim
{
    public class UpdatePhimResponseDTO
    {
        public int PhimId { get; set; }
        public string TenP { get; set; } = string.Empty;
        public string? MoTaP { get; set; }
        public string TenCTSXP { get; set; } = string.Empty;
        public DateTime NgayPhatHanhP { get; set; }
        public string ThuoiLuongP { get; set; } = string.Empty;
        public string AvatarP { get; set; } = string.Empty;
        public string AvatarPoster { get; set; } = string.Empty;
        public string NguonP { get; set; } = string.Empty;
        public string? SoTapP { get; set; }
        public string? TraiLerP { get; set; }
        public string QuocGia { get; set; } = string.Empty;
        public string NgonNgu { get; set; } = string.Empty;
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
