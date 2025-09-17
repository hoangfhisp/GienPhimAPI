using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Shared.Kernel;

namespace GienPhim_Services.UserService.Domain.Services.Interfaces
{
    public interface IUserDomainService
    {
        Task<User> RegisterUserAsync(User user, string password, CancellationToken cancellationToken = default); // Đăng ký User mới với vai trò mặc định
        Task<User> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default); // Đăng ký hoặc đăng nhập User từ bên ngoài với vai trò mặc định
        Task<User> ProviderRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default); // Đăng ký User mới từ bên ngoài với vai trò mặc định
        Task<User> ProviderLoginUserAsync(User user, CancellationToken cancellationToken = default); // Đăng nhập User từ bên ngoài
        Task<User> UpdateInfoUserAsync(InfoUserUpdateDTO update, string userId, CancellationToken cancellationToken = default); // Cập nhật thông tin User
        Task<User> LoginUserAsync(User user, LoginRequestDTO requestLogin, CancellationToken cancellationToken = default); // Đăng nhập User Local
        Task<User> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default); // Tìm User theo email
        Task<User> ResetPasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default); // Reset mật khẩu User
    }
}
