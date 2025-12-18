namespace GienPhim_Services.InteractionService.Application.DTOs.Client.Http
{
    public class PhimHttpResponseDTO
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
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
