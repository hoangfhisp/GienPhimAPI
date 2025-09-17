
using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.Factories.Interfaces;
using System.Security.Cryptography;

namespace GienPhim_Services.AuthService.Domain.Factories
{
    public class OtpFactory : IOtpFactory
    {
        public Otp Create(string userId, string type, int expiryMinutes = 5)
        {
            var code = GenerateOtpCode();
            return new Otp(userId, code, type, expiryMinutes);
        }

        private string GenerateOtpCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            var number = BitConverter.ToUInt32(bytes, 0) % 1000000; // 6 số
            return number.ToString("D6");
        }
    }
}
