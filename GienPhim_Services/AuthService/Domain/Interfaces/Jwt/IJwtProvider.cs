using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Domain.ValueObjects;
using System.Security.Claims;

namespace GienPhim_Services.AuthService.Domain.Interfaces.Jwt
{
    public interface IJwtProvider
    {
        Token GenerateToken(UserClaims userClaims); // Tạo token
        UserClaims? GetUserClaims(string token); // Lấy thông tin user từ token
        bool ValidateToken(string token); // Kiểm tra token có hợp lệ không
    }
}
