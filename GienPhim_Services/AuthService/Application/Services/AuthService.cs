using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Application.Interfaces.Orchestrators;
using GienPhim_Services.AuthService.Application.Interfaces.Services; 

namespace GienPhim_Services.AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthOrchestrator _authOrchestrator;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAuthOrchestrator authOrchestrator, ILogger<AuthService> logger)
        {
            _authOrchestrator = authOrchestrator;
            _logger = logger;
        }

        // Đăng nhập và tạo token cho người dùng
        public async Task<ResultDTO<GenerateTokenUserDTO>> LoginUserGenerateTokenAsync(LoginUserPasswordRequestDTO request ,CancellationToken cancellationToken = default)
        {
            return await _authOrchestrator.LoginUserGenerateTokenAsync(request, cancellationToken);
        }

        // Đăng nhập bằng phương thức bên ngoài sinh token
        public async Task<ResultDTO<GenerateTokenUserDTO>> LoginExternalGenerateTokenAsync(ExternalAuthTokenRequestDTO requestExternal, CancellationToken cancellationToken = default) // Nhà cung cấp , id user của bên cung cấp
        {
           return await _authOrchestrator.LoginExternalGenerateTokenAsync(requestExternal, cancellationToken);

        }

        // Gửi mã OTP đến email người dùng
        public async Task<ResultDTO> GenerateAndSendOtpEmailAsync(ForgotPasswordEmailRequestDTO forgot, CancellationToken cancellationToken = default)
        {
            return await _authOrchestrator.GenerateAndSendOtpEmailAsync(forgot, cancellationToken);
        }

        // Xác thực mã OTP cho việc quên mật khẩu
        public async Task<ResultDTO> VerifyForgotPasswordOtpAsync(VerifyOtpEmailRequestDTO request, CancellationToken cancellationToken = default)
        {
            return await _authOrchestrator.VerifyForgotPasswordOtpAsync(request, cancellationToken);
        }

        // Xác thực mã OTP và đặt lại mật khẩu
        public async Task<ResultDTO> VerifyOtpForResetPasswordAsync(VerifyOtpEmailResetPasswordRequestDTO request, CancellationToken cancellationToken = default)
        {
            return await _authOrchestrator.VerifyOtpForResetPasswordAsync(request, cancellationToken);
        }
    }
}
