using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Admin.User
{
    public class DeleteUserRequestDTO
    {
        [Required(ErrorMessage = "UserId là bắt buộc")]
        public string UserId { get; set; }
    }
}
