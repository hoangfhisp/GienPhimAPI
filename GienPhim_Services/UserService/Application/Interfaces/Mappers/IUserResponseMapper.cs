using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Mappers
{
    public interface IUserResponseMapper
    {
        UpdateInfoUserResponseDTO ToUpdate(User user); // Map Cập nhật thông tin user
        ExternalLoginUserResponseDTO ToExternalLogin(User user); // Map Đăng nhập bên ngoài
        LoginLocalUserResponseDTO ToLocalLogin(User user); // Map Đăng nhập local
        UserResponseDTO ToDto(User user); // Map User sang UserResponseDTO
    }
}
