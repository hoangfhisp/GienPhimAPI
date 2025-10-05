using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.MembershipService.Domain.Entities
{
    // Lớp này đại diện cho một kế hoạch thành viên trong hệ thống.
    [Table("MembershipPlan")]
    public class MembershipPlan
    {
        [Key]
        public string PlanId { get; set; } // Mã gói thành viên

        public string TenPlan { get; set; } // Tên gói thành viên
        public string PlanSlug { get; set; }
        public string? MoTaPlan { get; set; } // Mô tả gói thành viên

        public List<PlanOption> PlanOptions { get; set; } // Danh sách lựa chọn gói thành viên
    }
}
