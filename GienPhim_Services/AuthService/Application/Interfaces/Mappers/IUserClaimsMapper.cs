using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Application.DTOs.User;
using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Application.Interfaces.Mappers
{
    public interface IUserClaimsMapper
    {
        UserClaims ToDomain(UserClaimsDTO dto); // Chuyển từ DTO sang Domain
        UserClaimsDTO ToDTO(UserClaims domain); // Chuyển từ Domain sang DTO
        UserClaims FromUserServiceLoginResponse(LoginTokenResponseDTO user); // Chuyển từ phản hồi đăng nhập của UserService sang UserClaimsDTO
        UserClaims FormUserServiceUserResponseClaims(UserResponseDTO user); // Chuyển từ phản hồi thông tin người dùng của UserService sang UserClaimsDTO
    }
}
