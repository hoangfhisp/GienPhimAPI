namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class LichSuXem
    {
        public int LichSuXemId { get; set; }
        public int PhimId { get; set; }
        public Phim Phim { get; set; }
        public string UserId { get; set; }
        public string TapPhimId { get; set; }
        public string TapVuaXem { get; set; }
        public DateTime ThoiGianXem { get; set; } = DateTime.Now;
    }
}
