using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.DTOs.User;

namespace GienPhim_Services.AuthService.Application.Interfaces.Mappers
{
    public interface IUserHttpMapper
    {
        ResetPasswordEmailHttpRequestDTO ToResetPasswordDTO(VerifyOtpEmailResetPasswordRequestDTO request, string userId); // Chuyển đổi DTO yêu cầu xác minh OTP thành DTO yêu cầu đặt lại mật khẩu
        ChangeEmailHttpRequestDTO ToChangEmailDTO(VerifyChangeEmailRequestDTO request, string userId); // Chuyển đổi DTO yêu cầu đổi email thành DTO yêu cầu đổi email HTTP
    }
}
