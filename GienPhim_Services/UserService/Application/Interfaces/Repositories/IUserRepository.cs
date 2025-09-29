using GienPhim_Services.UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(User user,string password); // Đăng ký user
        Task<IdentityResult> RegisterUserNotPasswordAsync(User user);
        Task<IdentityResult> UpdateUserAsync(User user); // Update User
        Task<IdentityResult> AssignRoleToUserAsync(User user, string role); // Gán vai trò cho người dùng
        Task<IList<string>> GetRolesUserAsync(User user); // Lấy danh sách vai trò của người dùng
        Task<User> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<User?> FindByAuthIdAndTypeLoginAsync(string authId, string typeLogin, CancellationToken cancellationToken = default); // Tìm User Dựa vào AuthId và TypeLogin
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default); // Tìm User Dựa vào Email 
        Task<User> FindByUsernameAsync(string userName, CancellationToken cancellationToken = default); // Lấy User Dựa vào UserName
        Task<User?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default); // Lấy User Dựa vào SDT
        Task<bool> CheckPasswordAsync(User user, string password); // Check mật khẩu của User
        Task<string> GeneratePasswordResetTokenAsync(User user); // Tạo mã token để reset mật khẩu
        Task<IdentityResult> ResetPasswordWithTokenAsync(User user, string token, string newPassword); // Reset mật khẩu với token
        Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail); // Tạo mã token để đổi email
        Task<IdentityResult> ChangeEmailWithTokenAsync(User user, string newEmail, string token); // Đổi email với token
        Task<IdentityResult> DeleteUserAsync(string UserId); // Xóa user
    }
}
