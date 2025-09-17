using GienPhim_Services.AuthService.Domain.Entities;

namespace GienPhim_Services.AuthService.Domain.Factories.Interfaces
{
    public interface IOtpFactory
    {
        Otp Create(string userId, string type, int expiryMinutes = 5); // Tạo OTP mới

    }
}
