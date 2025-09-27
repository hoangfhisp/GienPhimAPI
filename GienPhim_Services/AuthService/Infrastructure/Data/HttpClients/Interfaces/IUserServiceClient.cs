using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.User;

namespace GienPhim_Services.AuthService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        // Gọi Api
        Task<ApiResponse<LoginTokenResponseDTO?>> LoginUserAsync(LoginUserPasswordRequestDTO request ,CancellationToken cancellationToken = default); // lOGIN USER 
        Task<ApiResponse<LoginTokenResponseDTO>?> ProviderLoginUserAsync(ExternalAuthUserInfoDTO request, CancellationToken cancellationToken = default); // Login User Google
        Task<ApiResponse<ResultDTO<UserResponseDTO>>?> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default); // Lấy thông tin user theo email
        Task<ApiResponse<ResultDTO>> ResetPasswordAsync(ResetPasswordEmailHttpRequestDTO requet, CancellationToken cancellationToken = default); // Reset mật khẩu
        Task<ApiResponse<ResultDTO<UserResponseDTO>>?> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Lấy thông tin user theo userId
    }
}
