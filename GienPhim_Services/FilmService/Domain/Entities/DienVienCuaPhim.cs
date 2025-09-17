using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class DienVienCuaPhim
    {
        [DisplayName("Chọn diễn viên")]
        [Required(ErrorMessage = "Hãy chọn ID diễn viên!")]
        public int DienVienId { get; set; }
        public DienVien DienVien { get; set; }

        [DisplayName("Chọn phim")]
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
