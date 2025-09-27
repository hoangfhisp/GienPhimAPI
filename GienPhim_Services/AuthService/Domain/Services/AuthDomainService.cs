using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.Exceptions.Failed;
using GienPhim_Services.AuthService.Domain.Exceptions.Invalid;
using GienPhim_Services.AuthService.Domain.Interfaces.Jwt;
using GienPhim_Services.AuthService.Domain.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Domain.ValueObjects;
using System.Threading.Tasks;

namespace GienPhim_Services.AuthService.Domain.Services
{
    public class AuthDomainService : IAuthDomainService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthDomainService(IJwtProvider jwtProvider, ITokenGenerator tokenGenerator)
        {
            _jwtProvider = jwtProvider;
            _tokenGenerator = tokenGenerator;
        }

        // Tạo Token Cho client
        public Token GenerateToken(UserClaims userClaims)
        {
            var token = _jwtProvider.GenerateToken(userClaims);
            if (token == null)
                throw new GenerateTokenUserFailedException();
            return token;
        }

        // Check xem token còn sử dụng được không
        public bool ValidateToken(string token)
        {
            var validate = _jwtProvider.ValidateToken(token);
            if (!validate)
                throw new InvalidTokenException();

            return validate;
        }

        // Tạo mã refresh token
        public RefreshTokenString GenarateRefreshToken()
        {
            var token = _tokenGenerator.GenerateTokenString();
            if(token == null || string.IsNullOrWhiteSpace(token))
                throw new GenerateRefreshTokenFailedException();
            
            return new RefreshTokenString(token);
        }

        // Kiểm tra xem mã refresh token còn hoạt động không
        public void IsActiveRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new InvalidRefreshTokenException();

            if (!refreshToken.IsActive)
                throw new InvalidExpiredRefreshTokenException();
        }

        public void RevokeRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new InvalidRefreshTokenException();
            if (!refreshToken.IsActive)
                throw new InvalidExpiredRefreshTokenException();
            
            refreshToken.RevokedAt = DateTime.UtcNow;
        }

        // Lấy thông tin user từ token
        public UserClaims GetUserFromToken(string token)
        {
            var user = _jwtProvider.GetUserClaims(token);
            if (user == null) // Token
                throw new InvalidTokenException();

            return user;
        }
    }
}
