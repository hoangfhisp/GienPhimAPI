namespace GienPhim_Services.AuthService.Application.Interfaces.Templates
{
    public interface IEmailTemplateService
    {
        string GetOtpSubject(string type);
        string GetOtpBody(string otpCode);
    }
}
