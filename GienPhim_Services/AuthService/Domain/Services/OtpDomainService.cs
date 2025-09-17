using GienPhim_Services.AuthService.Application.Interfaces.Repositories;
using GienPhim_Services.AuthService.Domain.Factories.Interfaces;
using GienPhim_Services.AuthService.Domain.Services.Interfaces;

namespace GienPhim_Services.AuthService.Domain.Services
{
    public class OtpDomainService : IOtpDomainService
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IOtpFactory _otpFactory;
        private readonly ILogger<OtpDomainService> _logger; // Mã OTP hợp lệ trong 5 phút

        public OtpDomainService(IOtpRepository otpRepository, IOtpFactory otpFactory
            , ILogger<OtpDomainService> logger)
        {
            _otpRepository = otpRepository;
            _otpFactory = otpFactory;
            _logger = logger;
        }

        // Tạo và lưu mã OTP
        public async Task<string> GenerateOtpAsync(string userId, string type, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainService: GenerateOtp cho user");
            var otp = _otpFactory.Create(userId,type);
            await _otpRepository.SaveAsync(otp, cancellationToken);
            _logger.LogInformation("DomainService: OTP của User {UserId} là {OtpCode}", userId, otp.Code);
            return otp.Code;
        }

        // Xác thực mã OTP
        public async Task<bool> VerifyOtpAsync(string userId, string type, string code, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainService: VerifyOtp cho user");
            var otp = await _otpRepository.GetAsync(userId, type, cancellationToken);
            if (otp == null)
            {
                _logger.LogWarning("DomainService: OTP không tồn tại hoặc đã hết hạn cho user {UserId}", userId);
                return false;
            }

            var valid = otp.Verify(code);
            _logger.LogInformation("DomainService: OTP của User {UserId} là {Status}", userId, valid ? "Hợp lệ" : "Không hợp lệ");
            return valid;
        }

        // Xác thực mã OTP và xóa nó nếu hợp lệ (dành cho việc đặt lại mật khẩu)
        public async Task<bool> VerifyOtpForResetAsync(string userId, string type, string code, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainService: VerifyOtpForReset cho user {UserId}", userId);
            var otp = await _otpRepository.GetAsync(userId, type, cancellationToken);
            if (otp == null)
            {
                _logger.LogWarning("DomainService: OTP không tồn tại hoặc đã hết hạn cho user {UserId}", userId);
                return false;
            }

            var valid = otp.Verify(code);
            if (valid)
            {
                _logger.LogInformation("DomainService: OTP của User {UserId} đã xóa sau khi hợp lệ", userId);
                await _otpRepository.RemoveAsync(userId, type, cancellationToken);
            }

            _logger.LogInformation("DomainService: OTP của User {UserId} là {Status}", userId, valid ? "Hợp lệ" : "Không hợp lệ");
            return valid;
        }
    }
}
