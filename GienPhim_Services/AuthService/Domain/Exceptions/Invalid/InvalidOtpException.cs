using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Invalid
{
    public class InvalidOtpException : DomainException
    {
        public InvalidOtpException(string code) : base($"Mã OTP {code} không hợp lệ hoặc đã hết hạn!")
        {
        }
    }
}
