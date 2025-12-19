using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership
{
    public class UserMembershipCreateDTO
    {
        [Required(ErrorMessage = "Thiếu OptionId")]
        public string OptionId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thiếu UserId")]
        public string UserId { get; set; } = string.Empty;

    }
}
