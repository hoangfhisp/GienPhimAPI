using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Domain.ValueObjects;

namespace GienPhim_Services.AuthService.Domain.Interfaces.Services
{
    public interface IAuthDomainService
    {
        Token GenerateToken(UserClaims userClaims); // Tạo Token Cho client
        RefreshTokenString GenarateRefreshToken(); // Tạo mã refresh token
        bool ValidateToken(string token); // Kiểm tra xem token có hợp lệ không
        void IsActiveRefreshToken(RefreshToken refreshToken); // Kiểm tra xem mã refresh token còn hoạt động không
        void RevokeRefreshToken(RefreshToken refreshToken); // Thu hồi mã refresh token
        UserClaims GetUserFromToken(string token); // Lấy thông tin người dùng từ token
    }
}
