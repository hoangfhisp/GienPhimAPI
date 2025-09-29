namespace GienPhim_Services.FilmService.Application.DTOs.Client.Phim
{
    public class PhimResponseDTO
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

        // Thống kê
        public long LuotThichP { get; set; }
        public long LuotDislikeP { get; set; }
        public long LuotXemP { get; set; }

        // Thông tin quan hệ
        public string QuocGia { get; set; } = string.Empty;
        public string NgonNgu { get; set; } = string.Empty;

        public List<string>? TheLoais { get; set; }
        public List<string>? DaoDiens { get; set; }
        public List<string>? DienViens { get; set; }
        public List<string>? TheTags { get; set; }
    }
}
