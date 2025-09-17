using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Domain.Models;

namespace GienPhim_Services.AuthService.Application.Interfaces.Mappers
{
    public interface IUserClaimsMapper
    {
        UserClaims ToDomain(UserClaimsDTO dto); // Chuyển từ DTO sang Domain
        UserClaimsDTO ToDTO(UserClaims domain); // Chuyển từ Domain sang DTO
    }
}
