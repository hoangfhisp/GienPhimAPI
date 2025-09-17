using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class PhanLoaiTheTag
    {
        [DisplayName("ID thẻ tag")]
        [Required(ErrorMessage = "Hãy chọn ID thẻ tag!")]
        public int TheTagId { get; set; }
        public TheTag TheTag { get; set; }

        [DisplayName("ID phim")]
        [Required(ErrorMessage = "Hãy chọn ID phim!")]
        public int PhimId { get; set; }
        public Phim Phim { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
