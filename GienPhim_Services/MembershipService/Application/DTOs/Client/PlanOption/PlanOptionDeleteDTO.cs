using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption
{
    public class PlanOptionDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id của lựa chọn gói thành viên")]
        public string OptionId { get; set; } = string.Empty;
    }
}
