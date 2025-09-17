using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Models.Entities
{
    public class PlanOption
    {
        [Key]
        public string OptionId { get; set; } // Mã lựa chọn gói TV

        public string PlanId { get; set; }
        [ForeignKey("PlanId")]
        public MembershipPlan MembershipPlan { get; set; } // Tham chiếu đến gói thành viên

        public int ThoiGianSuDung { get; set; } // Thời gian sử dụng gói thành viên (ngày)
        public decimal GiaOption { get; set; } // Giá gói thành viên

        public List<UserMembership> UserMemberships { get; set; } // Danh sách chi tiết gói thành viên
    }
}
