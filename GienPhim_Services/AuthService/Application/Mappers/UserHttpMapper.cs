using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;

namespace GienPhim_Services.AuthService.Application.Mappers
{
    public class UserHttpMapper : IUserHttpMapper
    {
        public ResetPasswordEmailHttpRequestDTO ToResetPasswordDTO(VerifyOtpEmailResetPasswordRequestDTO request, string userId)
        {
            return new ResetPasswordEmailHttpRequestDTO
            {
                UserId = userId,
                NewPassword = request.NewPassword,
                ConfirmNewPassword = request.ConfirmNewPassword
            };
        }
    }
}
