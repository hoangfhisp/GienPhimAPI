using GienPhim_Services.AuthService.Application.Interfaces.Templates;

namespace GienPhim_Services.AuthService.Application.Templates
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GetOtpSubject() => "Your OTP Code";

        public string GetOtpBody(string otpCode) =>
            $"Mã OTP của bạn là: {otpCode}. Có hiệu lực trong 5 phút.";
    }
}
