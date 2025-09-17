using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.ForgotPassword
{
    public class VerifyOtpEmailRequestDTO
    {
        [Required(ErrorMessage ="Email là bắt buộc!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mã OTP là bắt buộc!")]
        public string Otp { get; set; }

        [Required(ErrorMessage = "Loại OTP là bắt buộc!")]
        public string Type { get; set; }
    }
}
