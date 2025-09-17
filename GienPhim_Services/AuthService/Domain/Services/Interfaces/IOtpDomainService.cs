namespace GienPhim_Services.AuthService.Domain.Services.Interfaces
{
    public interface IOtpDomainService
    {
        Task<string> GenerateOtpAsync(string userId, string type, CancellationToken cancellationToken = default); // Tạo và lưu mã OTP
        Task<bool> VerifyOtpAsync(string userId, string type, string code, CancellationToken cancellationToken = default); // Xác thực mã OTP
        Task<bool> VerifyOtpForResetAsync(string userId, string type, string code, CancellationToken cancellationToken = default); // Xác thực mã OTP và xóa nó nếu hợp lệ (dành cho việc đặt lại mật khẩu)
    }
}
