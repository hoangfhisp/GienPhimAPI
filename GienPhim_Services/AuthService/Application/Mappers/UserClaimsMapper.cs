using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;
using GienPhim_Services.AuthService.Domain.Models;

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
    }
}
