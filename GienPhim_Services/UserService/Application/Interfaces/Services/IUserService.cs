using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultDTO> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default); // Đăng ký User
        Task<ResultDTO<ExternalLoginUserResponseDTO>> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default); // Đăng nhập hoặc đăng ký User bằng Google
        Task<ResultDTO<UpdateInfoUserResponseDTO>> UpdateInfoUserAsync(InfoUserUpdateDTO update,CancellationToken cancellationToken = default); // Update thông tin người dùng
        Task<ResultDTO<LoginLocalUserResponseDTO>> LoginUserAsync(LoginRequestDTO requestLogin,CancellationToken cancellationToken = default); // Đăng nhập User
        Task<ResultDTO<UserResponseDTO>> FindUserByEmailAsync(FindUserByEmailDTO dto, CancellationToken cancellationToken = default); // Tìm User theo email
        Task<ResultDTO> ResetPasswordAsync(ResetPasswordRequestDTO dto, CancellationToken cancellationToken = default); // Reset mật khẩu
    }
}
