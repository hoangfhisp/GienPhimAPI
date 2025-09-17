using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class TheTag
    {
        [DisplayName("ID thẻ tag")]
        public int TheTagId { get; set; }

        [DisplayName("Tên thẻ tag")]
        [Required(ErrorMessage = "Hãy nhập tên thẻ tag!")]
        public string TenT { get; set; }

        [DisplayName("Mô tả thẻ tag")]
        public string? MoTaT { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<PhanLoaiTheTag> PhanLoaiTheTags { get; set; }
    }
}
