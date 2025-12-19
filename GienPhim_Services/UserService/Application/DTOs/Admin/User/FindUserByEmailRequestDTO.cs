
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Admin.User
{
    public class FindUserByEmailRequestDTO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
    }
}
