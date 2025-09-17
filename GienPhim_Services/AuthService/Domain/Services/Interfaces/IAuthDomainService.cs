using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Domain.Services.Interfaces
{
    public interface IAuthDomainService
    {
        string GenerateToken(UserClaims userClaims); // Tạo Token Cho client
        bool ValidateToken(string token); // Kiểm tra xem token có hợp lệ không
        UserClaims GetUserFromToken(string token); // Lấy thông tin người dùng từ token
    }
}
