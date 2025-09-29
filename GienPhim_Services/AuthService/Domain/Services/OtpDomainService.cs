using GienPhim_Services.AuthService.Domain.Exceptions.Failed;
using GienPhim_Services.AuthService.Domain.Exceptions.Invalid;
using GienPhim_Services.AuthService.Domain.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Domain.Services
{
    public class OtpDomainService : IOtpDomainService
    {
        public bool VerifyOtp(Otp otp,string code)
        {
            if (otp == null) 
                throw new InvalidOtpException(code);
            return otp.Verify(code);

        }

        public Otp CreateOtp(string userId, string type)
        {
            var otp = Otp.Create(userId, type);
            if(otp == null)
                throw new CreateOtpFailedException();

            return otp;
        }
    }
}
