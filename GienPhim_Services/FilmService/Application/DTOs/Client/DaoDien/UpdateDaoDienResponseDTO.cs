namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien
{
    public class UpdateDaoDienResponseDTO
    {
        public int DaoDienId { get; set; } // Id đạo diễn
        public string TenDD { get; set; }  // Tên đạo diễn
        public string? MoTaDD { get; set; } // Mô tả đạo diễn
        public DateTime ThoiGianCapNhat { get; set; } // Thời gian cập nhật
        public DateTime ThoiGianTao { get; set; } // Thời gian tạo
    }
}
