using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Failed
{
    public class CreateOtpFailedException : DomainException
    {
        public CreateOtpFailedException() : base($"Tạo otp thất bại!")
        {
        }
    }
}
