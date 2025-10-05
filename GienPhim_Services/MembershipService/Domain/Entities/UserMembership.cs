using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.MembershipService.Domain.Entities
{
    public class UserMembership
    {
        [Key]
        public string UserMembershipId { get; set; } // Mã thành viên người dùng
        public string OptionId { get; set; } // Mã gói thành viên
        [ForeignKey("OptionId")]
        public PlanOption PlanOption { get; set; }// Tham chiếu đến gói thành viên
        public string UserId { get; set; } // Mã người dùng
        public DateTime NgayBatDau { get; set; } // Ngày bắt đầu sử dụng gói thành viên
        public DateTime NgayKetThuc { get; set; } // Ngày kết thúc sử dụng gói thành viên
        public string TrangThai { get; set; } // Trạng thái gói thành viên (Đang sử dụng, Hết hạn, Đã hủy)
    }
}
