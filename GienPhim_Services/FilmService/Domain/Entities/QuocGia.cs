using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class QuocGia
    {
        [DisplayName("ID quốc gia")]
        public int QuocGiaId { get; set; }

        [DisplayName("Tên quốc gia")]
        [Required(ErrorMessage = "Hãy nhập tên quốc gia!")]
        public string TenQG { get; set; }

        [DisplayName("Mô tả ngôn ngữ")]
        public string MoTaQG { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
