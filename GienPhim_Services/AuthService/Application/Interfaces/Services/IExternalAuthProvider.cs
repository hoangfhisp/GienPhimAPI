using GienPhim_Services.AuthService.Application.DTOs.Login;

namespace GienPhim_Services.AuthService.Application.Interfaces.Services
{
    public interface IExternalAuthProvider
    {
        string ProviderName { get; } // "google", "facebook"
        Task<ExternalLoginUserInfoDTO> ValidateAsync(string token);
    }
}
