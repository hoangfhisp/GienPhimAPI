using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;

namespace GienPhim_Services.AuthService.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ResultDTO<GenerateTokenUserDTO>> LoginUserGenerateTokenAsync(LoginUserPasswordRequestDTO request, CancellationToken cancellationToken = default);  // Đăng nhập và tạo token cho người dùng
        Task<ResultDTO<GenerateTokenUserDTO>> LoginExternalGenerateTokenAsync(ExternalAuthTokenRequestDTO requestExternal, CancellationToken cancellationToken = default); // Đăng nhập hoặc đăng ký bằng Google và tạo token cho người dùng
        Task<ResultDTO> GenerateAndSendOtpEmailAsync(ForgotPasswordEmailRequestDTO forgot, CancellationToken cancellationToken = default); // Gửi mã OTP đến email người dùng
        Task<ResultDTO> VerifyForgotPasswordOtpAsync(VerifyOtpEmailRequestDTO request, CancellationToken cancellationToken = default); // Xác thực mã OTP cho việc quên mật khẩu
        Task<ResultDTO> VerifyOtpForResetPasswordAsync(VerifyOtpEmailResetPasswordRequestDTO request, CancellationToken cancellationToken = default); // Xác thực mã OTP và đặt lại mật khẩu
    }
}
