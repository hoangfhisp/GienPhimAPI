namespace GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption
{
    public class PlanOptionResponseDTO
    {
        public string OptionId { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public int ThoiGianSuDung { get; set; }
        public decimal GiaOption { get; set; }
        public MembershipPlanResponseDTO MembershipPlans { get; set; } 
    }

    public class MembershipPlanResponseDTO
    {
        public string TenPlan { get; set; } // Tên gói thành viên
        public string PlanSlug { get; set; }
        public string? MoTaPlan { get; set; } // Mô tả gói thành viên

    }
}
