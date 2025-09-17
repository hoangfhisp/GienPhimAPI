using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.Interfaces.Orchestrators
{
    public interface IUserOrchestrator
    {
        Task<ResultDTO> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default); // Đăng ký User mới với vai trò mặc định
        Task<ResultDTO<ExternalLoginUserResponseDTO>> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default); // Đăng nhập hoặc đăng ký User mới từ bên ngoài với vai trò mặc định
        Task<ResultDTO<UpdateInfoUserResponseDTO>> UpdateInfoUserAsync(InfoUserUpdateDTO update, CancellationToken cancellationToken = default); // Cập nhật thông tin User
        Task<ResultDTO<LoginLocalUserResponseDTO>> LoginUserAsync(LoginRequestDTO requestLogin, CancellationToken cancellationToken = default); // Đăng nhập User Local
        Task<ResultDTO<UserResponseDTO>> FindUserByEmailAsync(FindUserByEmailDTO dto, CancellationToken cancellationToken = default); // Tìm User theo email
        Task<ResultDTO> ResetPasswordAsync(ResetPasswordRequestDTO dto, CancellationToken cancellationToken = default); // Reset mật khẩu User
    }
} 
