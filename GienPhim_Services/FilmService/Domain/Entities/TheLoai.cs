using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class TheLoai
    {
        [DisplayName("ID thể loại")]
        public int TheLoaiId { get; set; }

        [DisplayName("Tên thể loại")]
        [Required(ErrorMessage = "Hãy nhập tên thể loại")]
        public string TenTL { get; set; }

        [DisplayName("Mô tả thể loại")]
        public string? MoTa { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<PhanLoaiTheLoai> PhanLoaiTheLoais { get; set; }
    }
}
