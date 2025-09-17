using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.Interfaces.Services;
using Google.Apis.Auth;

namespace GienPhim_Services.AuthService.Infrastructure.Data.ExternalAuth
{
    public class GoogleAuthProvider : IExternalAuthProvider
    {
        public string ProviderName => "Google";
        public async Task<ExternalLoginUserInfoDTO> ValidateAsync(string token)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            return new ExternalLoginUserInfoDTO
            {
                AuthId = payload.Subject,
                Email = payload.Email,
                NameND = payload.Name,
                AvatarND = payload.Picture,
                Provider = ProviderName
            };
        }
    }
}
