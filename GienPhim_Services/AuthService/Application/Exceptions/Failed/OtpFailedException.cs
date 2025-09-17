using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class OtpFailedException : BusinessException
    {
        public override int StatusCode => 500;
        public OtpFailedException(string account) : base($"OTP user {account} không tồn tại hoặc đã hết hạn ")
        {
        }
    }
}
