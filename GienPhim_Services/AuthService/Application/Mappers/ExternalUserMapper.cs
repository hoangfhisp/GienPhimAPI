using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;

namespace GienPhim_Services.AuthService.Application.Mappers
{
    public class ExternalUserMapper : IExternalUserMapper
    {
        public ExternalAuthUserInfoDTO ToAuthDTO(ExternalLoginUserInfoDTO external)
        {
           return new ExternalAuthUserInfoDTO
           {
               Email = external.Email,
               AuthId = external.AuthId,
               NameND = external.NameND,
               AvatarND = external.AvatarND,
               TypeLogin = external.Provider
           };
        }
    }
}
