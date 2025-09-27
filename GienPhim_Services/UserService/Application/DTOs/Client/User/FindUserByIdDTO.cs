using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Client.User
{
    public class FindUserByIdDTO
    {
        [Required(ErrorMessage = "UserId bắt buộc nhập")]
        public string UserId { get; set; }
    }
}
