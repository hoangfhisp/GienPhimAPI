using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.Token
{
    public class RefreshTokenRequestDTO
    {
        [Required(ErrorMessage = "Refresh token là bắt buộc!.")]
        public string RefreshToken { get; set; }
    }
}
