namespace GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption
{
    public class UpdatePlanOptionResponseDTO
    {
        public string OptionId { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public int ThoiGianSuDung { get; set; }
        public decimal GiaOption { get; set; }
    }
}
