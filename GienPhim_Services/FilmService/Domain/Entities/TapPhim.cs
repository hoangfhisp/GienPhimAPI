using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class TapPhim
    {
        [Key]
        public string TapId { get; set; }

        public int PhimId { get; set; }
        public Phim Phim { get; set; }

        public string TenTap { get; set; }
        public string SlugTap { get; set; } // cách viết đơn giản không dấu không cách
        public string TenFile { get; set; }
        public string? LinkChinh { get; set; }
        public string LinkPhu { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

    }
}
