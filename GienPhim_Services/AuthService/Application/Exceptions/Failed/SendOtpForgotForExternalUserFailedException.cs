using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class SendOtpForgotForExternalUserFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode; 
        public SendOtpForgotForExternalUserFailedException(string account , string typeLogin)
        : base($"Không thể gửi OTP cho user {account} vì đăng nhập bằng nhà cung cấp {typeLogin} bên ngoài")
        {
        }
    }
}
