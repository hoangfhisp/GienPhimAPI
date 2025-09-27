using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Domain.Models;
using System.Security.Claims;

namespace GienPhim_Services.AuthService.Domain.Interfaces.Factories
{
    public interface IAuthClaimsFactory
    {
        IEnumerable<Claim> CreateClaims(UserClaims userClaims); // Tạo các claim cho token
    }
}
