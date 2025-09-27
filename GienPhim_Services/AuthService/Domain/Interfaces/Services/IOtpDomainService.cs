using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Domain.Interfaces.Services
{
    public interface IOtpDomainService
    {
        bool VerifyOtp(Otp otp, string code); // Xác thực mã OTP
        Otp CreateOtp(string userId, string type); // Tạo mã OTP mới
    }
}
