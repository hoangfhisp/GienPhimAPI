using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class DienVien
    {
        [DisplayName("ID diễn viên")]
        public int DienVienId { get; set; }

        [DisplayName("Tên diễn viên")]
        [Required(ErrorMessage = "Hãy nhập tên diễn viên!")]
        public string TenDV { get; set; }

        [DisplayName("Mô tả diễn viên")]
        public string? MoTaDV { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<DienVienCuaPhim> DienVienCuaPhims { get; set; }
    }
}
