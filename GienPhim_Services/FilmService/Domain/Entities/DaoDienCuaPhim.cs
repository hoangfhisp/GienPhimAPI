using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class DaoDienCuaPhim
    {
        [DisplayName("Chọn phim")]
        [Required(ErrorMessage = "Hãy chọn ID phim!")]
        public int PhimId { get; set; }

        [DisplayName("Tên phim")]
        [Required(ErrorMessage = "Hãy chọn tên phim!")]
        public Phim Phim { get; set; }

        [DisplayName("Chọn đạo diễn")]
        [Required(ErrorMessage = "Hãy chọn ID đạo diễn!")]
        public int DaoDienId { get; set; }

        [DisplayName("Tên đạo diễn")]
        [Required(ErrorMessage = "Hãy chọn tên đạo diễn!")]
        public DaoDien DaoDien { get; set; }

        [DisplayName("Vai trò đạo diễn")]
        [Required(ErrorMessage = "Hãy nhập vai trò đạo diễn!")]
        public string VaiTroDG { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
