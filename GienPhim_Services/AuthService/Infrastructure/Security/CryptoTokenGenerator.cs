using GienPhim_Services.AuthService.Domain.Interfaces.Services;
using System.Security.Cryptography;

namespace GienPhim_Services.AuthService.Infrastructure.Security
{
    public class CryptoTokenGenerator : ITokenGenerator
    {
        public string GenerateTokenString()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
