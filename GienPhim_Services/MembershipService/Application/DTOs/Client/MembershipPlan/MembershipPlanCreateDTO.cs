using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan
{
    public class MembershipPlanCreateDTO
    {
        [Required(ErrorMessage = "Tên gói thành viên không được để trống")]
        [MaxLength(DtoInvariants.TenPlanMaxLength, ErrorMessage = "Tên gói thành viên không được vượt quá {1} ký tự")]
        public string TenPlan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug của gói thành viên không được để trống")]
        [MaxLength(DtoInvariants.SlugMaxLength, ErrorMessage = "Slug không được vượt quá {1} ký tự")]
        public string PlanSlug { get; set; } = string.Empty;

        [MaxLength(DtoInvariants.MoTaPlanMaxLength, ErrorMessage = "Mô tả không được vượt quá {1} ký tự")]
        public string? MoTaPlan { get; set; }
    }
}
