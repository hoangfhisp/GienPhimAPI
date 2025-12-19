using GienPhim_Services.AuthService.Application.Interfaces.Factories;
using GienPhim_Services.AuthService.Application.Options;
using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace GienPhim_Services.AuthService.Application.Factories
{
    public class TokenFactory : ITokenFactory
    {

        public readonly IOptions<AuthOptions> _authOptions;

        public TokenFactory(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        public RefreshToken CreateRefreshToken (RefreshTokenString refreshToken,string userId)
        {
            return new RefreshToken
            {
                RFTokenId = Guid.NewGuid().ToString("N"),
                Token = refreshToken.Value,
                UserId = userId,
                ExpiredAt = DateTime.UtcNow.AddDays(_authOptions.Value.RefreshTokenExpireDays) // Mặc định mã refresh token hết hạn sau 7 ngày
            };
        }
    }
}
