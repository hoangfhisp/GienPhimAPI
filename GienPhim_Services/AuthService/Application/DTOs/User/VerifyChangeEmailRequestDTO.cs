using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.User
{
    public class VerifyChangeEmailRequestDTO
    {
        [Required(ErrorMessage = "NewEmail bắt buộc phải nhập!")]
        [EmailAddress(ErrorMessage = "NewEmail không đúng định dạng!")]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = "Otp là bắt buộc!")]
        public string Otp { get; set; }

        [Required(ErrorMessage = "Type Otp là bắt buộc!")]
        public string Type { get; set; } = "ChangeEmail";

    }
}
