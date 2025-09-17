using GienPhim_Services.AuthService.Application.DTOs.Common;
using GienPhim_Services.AuthService.Application.DTOs.ForgotPassword;
using GienPhim_Services.AuthService.Application.DTOs.Login;
using GienPhim_Services.AuthService.Application.DTOs.Token;
using GienPhim_Services.AuthService.Application.Exceptions.Failed;
using GienPhim_Services.AuthService.Application.Exceptions.Invalid;
using GienPhim_Services.AuthService.Application.Exceptions.NotFounds;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;
using GienPhim_Services.AuthService.Application.Interfaces.Orchestrators;
using GienPhim_Services.AuthService.Application.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Services.Interfaces;
using GienPhim_Services.AuthService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.AuthService.Application.Orchestrators
{
    public class AuthOrchestrator : IAuthOrchestrator
    {
        private readonly IUserServiceClient _userServiceClient;
        private readonly IEmailService _emailService;
        private readonly IAuthDomainService _authDomainService;
        private readonly IOtpDomainService _otpDomainService;
        private readonly IUserClaimsMapper _userClaimsMapper;
        private readonly IExternalUserMapper _externalUserMapper;
        private readonly IEnumerable<IExternalAuthProvider> _externalAuthProvider;
        private readonly ILogger<AuthOrchestrator> _logger;

        public AuthOrchestrator(IUserServiceClient userServiceClient , IAuthDomainService authDomainService
            , IUserClaimsMapper userClaimsMapper, IExternalUserMapper externalUserMapper 
            , IOtpDomainService otpDomainService , IEmailService emailService
            , IEnumerable<IExternalAuthProvider> externalAuthProviders, ILogger<AuthOrchestrator> logger)
        {
            _userServiceClient = userServiceClient;
            _authDomainService = authDomainService;
            _otpDomainService = otpDomainService;
            _userClaimsMapper = userClaimsMapper;
            _externalUserMapper = externalUserMapper;
            _externalAuthProvider = externalAuthProviders;
            _emailService = emailService;
            _logger = logger;
        }

        // Đăng nhập và tạo token cho người dùng bằng tài khoản và mật khẩu
        public async Task<ResultDTO<GenerateTokenUserDTO>> LoginUserGenerateTokenAsync(LoginUserPasswordRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: LoginUserGenerateTokenAsync gọi với: {Account}", request.Account);
            var userResponse = await _userServiceClient.LoginUserAsync(request,cancellationToken);
            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: LoginUserGenerateTokenAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }

            if (!userResponse.Data.Status)
            {
                _logger.LogWarning("Orchestrator: LoginUserGenerateTokenAsync đăng nhập thất bại với: {Account} , Lỗi: {Error}", request.Account, userResponse.Data.Msg);
                throw new LoginUserFailedException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            var claimsDTO =  new UserClaimsDTO
            {
                UserId = userResponse.Data.Data.UserId,
                Email = userResponse.Data.Data.Email,
                UserName = userResponse.Data.Data.UserName,
                Role = userResponse.Data.Data.Role
            };

            var claims = _userClaimsMapper.ToDomain(claimsDTO);

            // Tạo token
            var token = _authDomainService.GenerateToken(claims);
            if(token == null)
            {
                _logger.LogError("Orchestrator: LoginUserGenerateTokenAsync tạo token thất bại với: {Account}", request.Account);
                throw new GenerateTokenUserFailedException(request.Account);
            }

            var result = new GenerateTokenUserDTO
            {
                Token = token,
            };

            _logger.LogInformation("Orchestrator: LoginUserGenerateTokenAsync đăng nhập thành công với: {Account}", request.Account);
            return ResultDTO<GenerateTokenUserDTO>.Success(result, "Đăng nhập thành công");
        }

        // Đăng nhập hoặc đăng ký bằng Google và tạo token cho người dùng
        public async Task<ResultDTO<GenerateTokenUserDTO>> LoginExternalGenerateTokenAsync(ExternalAuthTokenRequestDTO requestExternal, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: LoginExternalGenerateTokenAsync gọi với: {Provider}", requestExternal.Provider);
            // tìm nhà cung cấp
            var externalProvider =  _externalAuthProvider.FirstOrDefault(p => p.ProviderName.Equals(requestExternal.Provider, StringComparison.OrdinalIgnoreCase)); // tìm không phân biệt chử hoa thường

            if (externalProvider == null)
            {
                _logger.LogWarning("Orchestrator: LoginExternalGenerateTokenAsync nhà cung cấp không hợp lệ: {Provider}", requestExternal.Provider);
                throw new InvalidAuthProviderException(requestExternal.Provider);
            }

            // xác thực token bên cung cấp
            var externalUser = await externalProvider.ValidateAsync(requestExternal.IdToken);
            if (externalUser == null)
            {
                _logger.LogWarning("Orchestrator: LoginExternalGenerateTokenAsync token không hợp lệ từ nhà cung cấp: {Provider}", requestExternal.Provider);
                throw new InvalidTokenException();
            }

            // ánh xạ sang DTO gửi đến UserService
            var request = _externalUserMapper.ToAuthDTO(externalUser);

            // gửi yêu cầu đăng nhập hoặc đăng ký đến UserService
            var userResponse = await _userServiceClient.ProviderLoginUserAsync(request,cancellationToken);
            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: LoginExternalGenerateTokenAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }
            // nếu lỗi báo lỗi
            if (!userResponse.Data.Status)
            {
                _logger.LogWarning("Orchestrator: LoginExternalGenerateTokenAsync đăng nhập thất bại với: {Email} , Lỗi: {Error}", request.Email, userResponse.Data.Msg);
                throw new LoginUserFailedException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            var claimsDTO = new UserClaimsDTO
            {
                UserId = userResponse.Data.Data.UserId,
                Email = userResponse.Data.Data.Email,
                UserName = userResponse.Data.Data.UserName,
                Role = userResponse.Data.Data.Role
            };

            var claims = _userClaimsMapper.ToDomain(claimsDTO);

            // Tạo token
            var token = _authDomainService.GenerateToken(claims);
            if (token == null)
            {
                _logger.LogError("Orchestrator: LoginExternalGenerateTokenAsync tạo token thất bại với: {Account}", request.Email);
                throw new GenerateTokenUserFailedException(request.Email);
            }

            var result = new GenerateTokenUserDTO
            {
                Token = token,
            };

            _logger.LogInformation("Orchestrator: LoginExternalGenerateTokenAsync đăng nhập thành công với: {Account}", request.Email);
            return ResultDTO<GenerateTokenUserDTO>.Success(result, "Đăng nhập thành công");
        }

        public async Task<ResultDTO> GenerateAndSendOtpEmailAsync(ForgotPasswordEmailRequestDTO forgot, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: bắt đầu tạo OTP cho user {Email}", forgot.Email);
            var userResponse = await _userServiceClient.FindUserByEmailAsync(forgot.Email, cancellationToken);

            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: LoginUserGenerateTokenAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }

            if (!userResponse.Data.Status)
            {
                _logger.LogError("Orchestrator: không tìm thấy user với email {Email}", forgot.Email);
                throw new UserNotFoundException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            if (userResponse.Data.Data.TypeLogin != "Local")
            {
                _logger.LogError("Orchestrator: không thể gửi OTP cho user {Email} vì đăng nhập bằng nhà cung cấp bên ngoài", forgot.Email);
                throw new SendOtpForgotForExternalUserFailedException(forgot.Email,userResponse.Data.Data.TypeLogin);
            }

            var otpCode = await _otpDomainService.GenerateOtpAsync(userResponse.Data.Data.UserId, forgot.Type, cancellationToken);

            await _emailService.SendAsync(
                userResponse.Data.Data.Email,
                "Your OTP Code",
                $"Mã OTP của bạn là: {otpCode}. Có hiệu lực trong 5 phút.");

            _logger.LogInformation("Orchestrator: đã gửi OTP qua email {Email}", userResponse.Data.Data.Email);
            return ResultDTO.Success($"OTP đã được gửi đến: {userResponse.Data.Data.Email}");
        }

        // Xác thực mã OTP
        public async Task<ResultDTO> VerifyForgotPasswordOtpAsync(VerifyOtpEmailRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: bắt đầu xác thực OTP cho user {Email}", request.Email);
            var userResponse = await _userServiceClient.FindUserByEmailAsync(request.Email, cancellationToken);

            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: VerifyForgotPasswordOtpAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }
            if (!userResponse.Data.Status)
            {
                _logger.LogError("Orchestrator: không tìm thấy user với email {Email}", request.Email);
                throw new UserNotFoundException(userResponse.Data.Msg, userResponse.StatusCode);
            }
            var valid = await _otpDomainService.VerifyOtpAsync(userResponse.Data.Data.UserId, request.Type, request.Otp, cancellationToken);

            if (!valid)
            {
                _logger.LogWarning("Orchestrator: OTP không hợp lệ hoặc đã hết hạn cho user {Email}", request.Email);
                throw new OtpFailedException(request.Email);
            }

            _logger.LogInformation("Orchestrator: OTP hợp lệ cho user {Email}", request.Email);
            return ResultDTO.Success("Xác thực OTP thành công");

        }

        public async Task<ResultDTO> VerifyOtpForResetPasswordAsync(VerifyOtpEmailResetPasswordRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: bắt đầu xác thực OTP cho user {Email}", request.Email);
            var userResponse = await _userServiceClient.FindUserByEmailAsync(request.Email, cancellationToken);
            if (userResponse == null)
            {
                _logger.LogError("Orchestrator: VerifyForgotPasswordOtpAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }
            if (!userResponse.Data.Status)
            {
                _logger.LogError("Orchestrator: không tìm thấy user với email {Email}", request.Email);
                throw new UserNotFoundException(userResponse.Data.Msg, userResponse.StatusCode);
            }
            var valid = await _otpDomainService.VerifyOtpForResetAsync(userResponse.Data.Data.UserId, request.Type, request.Otp, cancellationToken);
            if (!valid)
            {
                _logger.LogWarning("Orchestrator: OTP không hợp lệ hoặc đã hết hạn cho user {Email}", request.Email);
                throw new OtpFailedException(request.Email);
            }

            var resetRequest = new ResetPasswordEmailHttpRequestDTO
            {
                UserId = userResponse.Data.Data.UserId,
                NewPassword = request.NewPassword,
                ConfirmNewPassword = request.ConfirmNewPassword
            };

            var result = await _userServiceClient.ResetPasswordAsync(resetRequest,cancellationToken);
            if (result == null)
            {
                _logger.LogError("Orchestrator: VerifyForgotPasswordOtpAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }

            if (!result.Data.Status)
            {
                _logger.LogWarning("Orchestrator: ResetPasswordAsync thất bại cho user {Email} với lỗi {Error}", request.Email, result.Data.Msg);
                throw new ResetPasswordFailedException(result.Data.Msg, result.StatusCode);
            }

            _logger.LogInformation("Orchestrator: OTP hợp lệ cho user {Email}", request.Email);
            return ResultDTO.Success(result.Data.Msg);
        }
    }
}
