using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Application.DTOs.User;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.AuthService.Application.Mappers
{
    public class UserClaimsMapper : IUserClaimsMapper
    {
        public UserClaims ToDomain(UserClaimsDTO dto)
        {
            return new UserClaims(dto.UserId, dto.UserName, dto.Email, dto.Role);
        }

        public UserClaimsDTO ToDTO(UserClaims domain)
        {
            return new UserClaimsDTO
            {
                UserId = domain.UserId,
                UserName = domain.UserName,
                Email = domain.Email,
                Role = domain.Role
            };
        }
        public UserClaims FromUserServiceLoginResponse(LoginTokenResponseDTO user)
        {
            return new UserClaims(user.Data.UserId, user.Data.UserName, user.Data.Email, user.Data.Role);
               
        }
       
        public UserClaims FormUserServiceUserResponseClaims (UserResponseDTO user)
        {
            return new UserClaims(user.UserId, user.UserName, user.Email, user.Role);
        }
    }
}
