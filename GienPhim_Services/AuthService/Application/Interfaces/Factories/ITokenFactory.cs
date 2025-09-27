using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.ValueObjects;

namespace GienPhim_Services.AuthService.Application.Interfaces.Factories
{
    public interface ITokenFactory
    {
        RefreshToken CreateRefreshToken(RefreshTokenString refreshToken, string userId); // Tạo đối tượng RefreshToken từ chuỗi refresh token và userId
    }
}
