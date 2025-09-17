using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class FilmView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FilmViewId { get; set; }

        public int PhimId { get; set; }
        public Phim Phim { get; set; }

        public string? UserId { get; set; }
        public DateTime ThoiGianXem { get; set; }
    }
}
