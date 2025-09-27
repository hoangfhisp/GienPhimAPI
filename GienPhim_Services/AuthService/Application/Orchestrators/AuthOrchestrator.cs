using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.Exceptions.Failed;
using GienPhim_Services.AuthService.Application.Exceptions.NotFounds;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;
using GienPhim_Services.AuthService.Application.Interfaces.Orchestrators;
using GienPhim_Services.AuthService.Domain.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Models;
using GienPhim_Services.AuthService.Infrastructure.Data.HttpClients.Interfaces;
using System.Threading;

namespace GienPhim_Services.AuthService.Application.Orchestrators
{
    public class AuthOrchestrator : IAuthOrchestrator
    {
        private readonly IUserServiceClient _userServiceClient;
        private readonly IUserClaimsMapper _userClaimsMapper;
        private readonly ILogger<AuthOrchestrator> _logger;

        public AuthOrchestrator(IUserServiceClient userServiceClient
            , IUserClaimsMapper userClaimsMapper
            , ILogger<AuthOrchestrator> logger)
        {
            _userServiceClient = userServiceClient;
            _userClaimsMapper = userClaimsMapper;
            _logger = logger;
        }

        // Đăng nhập người dùng bằng tài khoản và mật khẩu
        public async Task<UserClaims> FetchUserClaimsForLoginAsync(LoginUserPasswordRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: FetchUserClaimsForLoginAsync gọi với: {Account}", request.Account);
            var userResponse = await _userServiceClient.LoginUserAsync(request, cancellationToken);
            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: FetchUserClaimsForLoginAsync không thể kết nối đến UserService với: {Account}", request.Account);
                throw new ConnectServiceFailedException("UserSerivce");
            }

            if (!userResponse.Data.Status)
            {
                _logger.LogWarning("Orchestrator: FetchUserClaimsForLoginAsync đăng nhập không thành công với: {Account}, Lỗi: {Error}", request.Account, userResponse.Data.Msg);
                throw new LoginUserFailedException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            var claims = _userClaimsMapper.FromUserServiceLoginResponse(userResponse.Data);
            _logger.LogInformation("Orchestrator: FetchUserClaimsForLoginAsync đăng nhập thành công với: {Account}", request.Account);
            return claims;
        }

        // Lấy thông tin user từ dịch vụ UserService theo userId
        public async Task<UserClaims> FetchUserClaimsByIdAsync(string userId , CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FetchUserClaimsByIdAsync với userId = {UserId}", userId);
            // Lấy thông tin user từ refresh token
            var user = await _userServiceClient.FindUserByUserIdAsync(userId, cancellationToken);
            if (user == null || user.Data == null)
            {
                _logger.LogError("Orchestrator: FetchUserClaimsByIdAsync không tìm thấy userId = {UserId}", userId);
                throw new UserNotFoundException(userId);
            }

            // Map sang domain claims
            var claims = _userClaimsMapper.FormUserServiceUserResponseClaims(user.Data.Data);
            return claims;
        }

        public async Task<UserClaims> FetchUserClaimsFromExternalAsync(ExternalAuthUserInfoDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: FetchUserClaimsFromExternalAsync gọi với: {Provider}", request.TypeLogin);
            // gửi yêu cầu đăng nhập hoặc đăng ký đến UserService
            var userResponse = await _userServiceClient.ProviderLoginUserAsync(request, cancellationToken);
            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: FetchUserClaimsFromExternalAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }
            // nếu lỗi báo lỗi
            if (!userResponse.Data.Status)
            {
                _logger.LogWarning("Orchestrator: FetchUserClaimsFromExternalAsync đăng nhập thất bại với: {Email} , Lỗi: {Error}", request.Email, userResponse.Data.Msg);
                throw new LoginUserFailedException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            var claims = _userClaimsMapper.FromUserServiceLoginResponse(userResponse.Data);
            _logger.LogInformation("Orchestrator: FetchUserClaimsFromExternalAsync đăng nhập thành công với: {Provider}", request.TypeLogin);

            return claims;
        }
    }
}
