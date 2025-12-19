using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan
{
    public class MembershipPlanDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu mã gói thành viên")]
        public string PlanId { get; set; } = string.Empty;
    }
}
