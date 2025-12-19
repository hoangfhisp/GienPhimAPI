using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan
{
    public class MembershipPlanResponseDTO
    {
        public string PlanId { get; set; } = string.Empty;
        public string TenPlan { get; set; } = string.Empty;
        public string PlanSlug { get; set; } = string.Empty;
        public string? MoTaPlan { get; set; }
        public List<PlanOptionResponseDTO> Options { get; set; } = new List<PlanOptionResponseDTO>();

    }

    public class PlanOptionResponseDTO
    {
        public string OptionId { get; set; } // Mã lựa chọn gói TV
        public int ThoiGianSuDung { get; set; } // Thời gian sử dụng gói thành viên (ngày)
        public decimal GiaOption { get; set; } // Giá gói thành viên
    }
}
