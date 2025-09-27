using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Domain.ValueObjects;

namespace GienPhim_Services.AuthService.Application.Interfaces.Orchestrators
{
    public interface IAuthOrchestrator
    {
        Task<UserClaims> FetchUserClaimsForLoginAsync(LoginUserPasswordRequestDTO request, CancellationToken cancellationToken = default); // Lấy thông tin user từ dịch vụ UserService khi đăng nhập bằng tài khoản và mật khẩu
        Task<UserClaims> FetchUserClaimsByIdAsync(string userId, CancellationToken cancellationToken = default); // Lấy thông tin user từ dịch vụ UserService theo userId
        Task<UserClaims> FetchUserClaimsFromExternalAsync(ExternalAuthUserInfoDTO request, CancellationToken cancellationToken = default); // Lấy thông tin user từ dịch vụ UserService khi đăng nhập bằng bên thứ 3
    }
}
