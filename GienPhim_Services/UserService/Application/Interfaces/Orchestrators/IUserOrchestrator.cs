using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Orchestrators
{
    public interface IUserOrchestrator
    {
        Task<User> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default); // Đăng ký User mới với vai trò mặc định
        Task<User> ProviderRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default); // Đăng ký User mới từ bên ngoài với vai trò mặc định
        User ProviderLoginUser(ExternalLoginUserInfoDTO request); // Đăng nhập User từ bên ngoài
        Task<User> UpdateInfoUserAsync(InfoUserUpdateDTO update, CancellationToken cancellationToken = default); // Cập nhật thông tin User
        Task<User> LoginLocalUserAsync(LoginRequestDTO requestLogin, CancellationToken cancellationToken = default); // Đăng nhập User Local
        User RepareUserByEmail(FindUserByEmailDTO dto); // Tìm User theo email
        User RepareUserByUserId(FindUserByIdDTO dto); // Tìm User theo userId
        User ResetPasswordAsync(ResetPasswordRequestDTO dto); // Reset mật khẩu User
        User ChangeEmail(User user, string newEmail); // Đổi email User
    }
} 
