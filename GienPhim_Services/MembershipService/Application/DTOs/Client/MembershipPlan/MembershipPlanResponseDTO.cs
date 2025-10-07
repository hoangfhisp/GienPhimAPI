namespace GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan
{
    public class MembershipPlanResponseDTO
    {
        public string PlanId { get; set; } = string.Empty;
        public string TenPlan { get; set; } = string.Empty;
        public string PlanSlug { get; set; } = string.Empty;
        public string? MoTaPlan { get; set; }
    }
}
