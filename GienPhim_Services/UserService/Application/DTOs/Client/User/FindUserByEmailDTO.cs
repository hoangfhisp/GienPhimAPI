using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Client.User
{
    public class FindUserByEmailDTO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
    }
}
