namespace GienPhim_Services.AuthService.Application.DTOs.ForgotPassword
{
    public class ResetPasswordEmailRequestDTO
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
}
