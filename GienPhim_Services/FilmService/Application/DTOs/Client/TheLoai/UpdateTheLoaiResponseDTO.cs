namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai
{
    public class UpdateTheLoaiResponseDTO
    {
        public int TheLoaiId { get; set; }      // Id thể loại
        public string TenTL { get; set; }       // Tên thể loại
        public string? MoTa { get; set; }       // Mô tả thể loại
        public DateTime ThoiGianCapNhat { get; set; } // Thời gian cập nhật
        public DateTime ThoiGianTao { get; set; }     // Thời gian tạo
    }
}
