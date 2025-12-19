namespace GienPhim_Services.FilmService.Application.DTOs.Client.TheTag
{
    public class UpdateTheTagResponseDTO
    {
        public int TheTagId { get; set; }           // Id thẻ tag
        public string TenT { get; set; }            // Tên thẻ tag
        public string? MoTaT { get; set; }          // Mô tả thẻ tag
        public DateTime ThoiGianCapNhat { get; set; } // Thời gian cập nhật
        public DateTime ThoiGianTao { get; set; }     // Thời gian tạo
    }
}
