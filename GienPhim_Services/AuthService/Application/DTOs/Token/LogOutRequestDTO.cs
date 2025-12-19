using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.Token
{
    public class LogOutRequestDTO
    {
        [Required(ErrorMessage ="Refresh Token là bắt buộc!")]
        public string RefreshToken { get; set; }
    }
}
