using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Admin.Role
{
    public class RoleCreateDTO
    {
        [Required(ErrorMessage = "Tên Role không được để trống")]
        public string RoleName { get; set; } // Name Role
    }
}
