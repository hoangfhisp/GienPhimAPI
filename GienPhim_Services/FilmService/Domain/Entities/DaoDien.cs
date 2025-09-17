using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class DaoDien
    {
        [DisplayName("ID đạo diễn")]
        public int DaoDienId { get; set; }

        [DisplayName("Tên đạo diễn")]
        [Required(ErrorMessage = "Hãy nhập tên đạo diễn!")]
        public string TenDD { get; set; }

        [DisplayName("Mô tả đạo diễn")]
        public string? MoTaDD { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<DaoDienCuaPhim> DaoDienCuaPhims { get; set; }
    }
}
