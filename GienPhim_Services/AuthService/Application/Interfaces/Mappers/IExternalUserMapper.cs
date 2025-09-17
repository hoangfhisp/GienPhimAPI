using GienPhim_Services.AuthService.Application.DTOs.Login;

namespace GienPhim_Services.AuthService.Application.Interfaces.Mappers
{
    public interface IExternalUserMapper 
    {
        ExternalAuthUserInfoDTO ToAuthDTO(ExternalLoginUserInfoDTO external); // chuyển từ DTO bên cung cấp về DTO bên mình
    }
}
