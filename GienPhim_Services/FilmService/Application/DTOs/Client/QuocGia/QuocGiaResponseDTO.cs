namespace GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia
{
    public class QuocGiaResponseDTO
    {
        public int QuocGiaId { get; set; }  // Id quốc gia
        public string TenQG { get; set; }   // Tên quốc gia
        public string? MoTaQG { get; set; } // Mô tả quốc gia
    }
}
