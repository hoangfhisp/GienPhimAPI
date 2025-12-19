using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.User
{
    public class ChangeEmailOtpRequestDTO
    {
        [Required(ErrorMessage = "Kiểu Otp đổi email là bắt buộc")]
        public string Type { get; set; } = "ChangeEmail";
    }
}
