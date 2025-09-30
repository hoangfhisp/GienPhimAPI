namespace GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu
{
    public class UpdateNgonNguResponseDTO
    {
        public int NgonNguId { get; set; } // Id ngôn ngữ
        public string TenNN { get; set; }  // Tên ngôn ngữ
        public string? MoTaNN { get; set; } // Mô tả ngôn ngữ
        public DateTime ThoiGianCapNhat { get; set; } // Thời gian cập nhật
        public DateTime ThoiGianTao { get; set; } // Thời gian tạo
    }
}
