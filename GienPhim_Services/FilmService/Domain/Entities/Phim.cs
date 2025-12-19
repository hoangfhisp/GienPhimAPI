using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.FilmService.Domain.Entities
{
    public class Phim
    {
        [DisplayName("ID phim")]
        public int PhimId { get; set; }

        [DisplayName("Tên phim")]
        [Required(ErrorMessage = "Hãy nhập tên phim!")]
        public string TenP { get; set; }

        [DisplayName("Mô tả phim")]
        public string? MoTaP { get; set; }

        [DisplayName("Quốc gia")]
        public int QuocGiaId { get; set; }
        public QuocGia QuocGia { get; set; }

        [DisplayName("Tên công ty sản xuất phim")]
        [Required(ErrorMessage = "Hãy nhập tên công ty sản xuất phim!")]
        public string TenCTSXP { get; set; }

        [DisplayName("Ngôn ngữ phim")]
        public int NgonNguId { get; set; }
        public NgonNgu NgonNgu { get; set; }

        [DisplayName("Ngay phát hành phim")]
        [Required(ErrorMessage = "Hãy chọn ngày phát hành phim!")]
        public DateTime NgayPhatHanhP { get; set; }

        [DisplayName("Lượt thích phim")]
        public long LuotThichP { get; set; }

        [DisplayName("Lượt dislike phim")]
        public long LuotDislikeP { get; set; }

        [DisplayName("Lượt xem phim")]
        public long LuotXemP { get; set; }

        [DisplayName("Thời lượng phim")]
        [Required(ErrorMessage = "Hãy nhập thời lượng phim!")]
        public string ThuoiLuongP { get; set; }

        [DisplayName("Ảnh phim")]
        [Required(ErrorMessage = "Hãy nhập ảnh phim!")]
        public string AvatarP { get; set; }

        [DisplayName("Poster phim")]
        [Required(ErrorMessage = "Hãy nhập poster phim!")]
        public string AvatarPoster { get; set; }

        [DisplayName("Nguồn phim")]
        [Required(ErrorMessage = "Hãy nhập nguồn phim!")]
        public string NguonP { get; set; }
        public string? SoTapP { get; set; }
        public string? TraiLerP { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời gian cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }

        public List<DaoDienCuaPhim> DaoDienCuaPhims { get; set; } = new();
        public List<PhanLoaiTheTag> PhanLoaiTheTags { get; set; } = new();
        public List<PhanLoaiTheLoai> PhanLoaiTheLoais { get; set; } = new();
        public List<DienVienCuaPhim> DienVienCuaPhims { get; set; } = new();
        public List<TapPhim> TapPhims { get; set; } = new();
        public List<LichSuXem> LichSuXems { get; set; } = new();
        public List<FilmView> FilmViews { get; set; } = new();

    }
}
