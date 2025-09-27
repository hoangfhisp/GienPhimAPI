using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;

namespace GienPhim_Services.AuthService.Application.Interfaces.Mappers
{
    public interface IUserHttpMapper
    {
        ResetPasswordEmailHttpRequestDTO ToResetPasswordDTO(VerifyOtpEmailResetPasswordRequestDTO request, string userId);
    }
}
