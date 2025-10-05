using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership
{
    public class UserMembershipDeleteDTO
    {
        [Required(ErrorMessage = "Thiếu Id của UserMembership")]
        public string UserMembershipId { get; set; } = string.Empty;
    }
}
