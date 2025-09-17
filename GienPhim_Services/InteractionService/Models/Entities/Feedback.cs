using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.InteractionService.Models.Entities
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FeedbackId { get; set; }
        [DisplayName("ID phim")]
        [Required(ErrorMessage = "Hãy chọn ID phim!")]
        public int PhimId { get; set; }

        [DisplayName("ID người dùng")]
        [Required(ErrorMessage = "Hãy chọn ID người dùng!")]
        public string UserId { get; set; }

        [ForeignKey("NguoiDungId")] // Thêm ràng buộc khóa ngoại

        [DisplayName("Lượt thích Comment")]
        public string? CommentId { get; set; }
        public Comment? Comment { get; set; }

        [DisplayName("Lượt thích phim")]
        public bool LuotThichP { get; set; }

        [DisplayName("Lượt dislike phim")]
        public bool LuotDislikeP { get; set; }

        [DisplayName("Thời gian tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianTao { get; set; }

        [DisplayName("Thời gian cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
