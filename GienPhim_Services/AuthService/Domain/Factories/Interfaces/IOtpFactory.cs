using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Domain.Factories.Interfaces
{
    public interface IOtpFactory
    {
        Otp Create(string userId, string type, int expiryMinutes = 5); // Tạo OTP mới

    }
}
