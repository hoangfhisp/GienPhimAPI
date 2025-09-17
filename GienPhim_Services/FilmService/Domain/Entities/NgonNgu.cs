using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class NgonNgu
    {
        [DisplayName("ID ngôn ngử")]
        public int NgonNguId { get; set; }

        [DisplayName("Tên ngôn ngữ")]
        [Required(ErrorMessage = "Hãy nhập tên ngôn ngữ!")]
        public string TenNN { get; set; }

        [DisplayName("Mô tả ngôn ngữ")]
        public string MoTaNN { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<Phim> Phims { get; set; }
    }
}
