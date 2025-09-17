using GienPhim_Services.AuthService.Application.Exceptions.Invalid;
using GienPhim_Services.AuthService.Application.Options;
using GienPhim_Services.AuthService.Domain.Factories.Interfaces;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Domain.Services.Interfaces;
using GienPhim_Services.AuthService.Infrastructure.Jwt;
using System.Security.Claims;

namespace GienPhim_Services.AuthService.Domain.Services
{
    public class AuthDomainService : IAuthDomainService
    {
        private readonly JwtOptions _jwtSettings;
        private readonly IAuthClaimsFactory _authClaimsFactory;
        private readonly ILogger<AuthDomainService> _logger;
        public AuthDomainService(JwtOptions jwtSettings, IAuthClaimsFactory authClaimsFactory
            , ILogger<AuthDomainService> logger)
        {
            _jwtSettings = jwtSettings;
            _authClaimsFactory = authClaimsFactory;
            _logger = logger;
        }

        // Tạo Token Cho client
        public string GenerateToken(UserClaims userClaims)
        {
            _logger.LogInformation("DomainService: Generating token cho user {UserId}", userClaims.UserId);
            var claims = _authClaimsFactory.CreateClaims(userClaims);
            return JwtUtils.GenerateToken(claims, _jwtSettings);
        }

        // Check xem token còn sử dụng được không
        public bool ValidateToken(string token)
        {
            _logger.LogInformation("DomainService: Validating token user {token}",token);
            return JwtUtils.ValidateToken(token, _jwtSettings) != null;
        }

        // Lấy thông tin user từ token
        public UserClaims GetUserFromToken(string token)
        {
            var principal = JwtUtils.ValidateToken(token, _jwtSettings);
            if (principal == null) // Token
                throw new InvalidTokenException();

            return new UserClaims
            (
                principal.FindFirstValue(ClaimTypes.NameIdentifier)!,
                principal.FindFirstValue(ClaimTypes.Name)!,
                principal.FindFirstValue(ClaimTypes.Email),
                principal.FindFirstValue(ClaimTypes.Role)!

            );
        }
    }
}
