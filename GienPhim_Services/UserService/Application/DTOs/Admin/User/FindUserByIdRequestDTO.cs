using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Admin.User
{
    public class FindUserByIdRequestDTO
    {
        [Required(ErrorMessage = "UserId bắt buộc nhập")]
        public string UserId { get; set; }
    }
}
