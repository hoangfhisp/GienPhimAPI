namespace GienPhim_Services.FilmService.Application.DTOs.Client.FilmView
{
    public class FilmViewResponseDTO
    {
        public string FilmViewId { get; set; } = string.Empty;
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public DateTime ThoiGianXem { get; set; }
    }
}
