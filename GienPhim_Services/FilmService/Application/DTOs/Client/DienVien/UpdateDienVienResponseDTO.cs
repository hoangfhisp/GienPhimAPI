namespace GienPhim_Services.FilmService.Application.DTOs.Client.DienVien
{
    public class UpdateDienVienResponseDTO
    {
        public int DienVienId { get; set; }      // Id diễn viên
        public string TenDV { get; set; }        // Tên diễn viên
        public string? MoTaDV { get; set; }      // Mô tả diễn viên
        public DateTime ThoiGianCapNhat { get; set; } // Thời gian cập nhật
        public DateTime ThoiGianTao { get; set; }     // Thời gian tạo
    }
}
