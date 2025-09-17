using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.Exceptions.NotFounds;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Application.Interfaces.Mappers;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Services.Interfaces;
using System.Security.Claims;

namespace GienPhim_Services.UserService.Application.Orchestrators
{
    public class UserOrchestrator : IUserOrchestrator
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IValidationService _validationService;
        private readonly IUserFactory _userFactory;
        private readonly IUserResponseMapper _userResponseMapper;
        private readonly IGuidUserNameGenerator _guidUserNameGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserOrchestrator> _logger;
        public UserOrchestrator(IUserDomainService userDomainService, IValidationService validationService
            , IUserFactory userFactory, IUserResponseMapper userResponseMapper
            , IGuidUserNameGenerator guidUserNameGenerator
            , IHttpContextAccessor httpContextAccessor, ILogger<UserOrchestrator> logger)
        {
            _userDomainService = userDomainService;
            _validationService = validationService;
            _userFactory = userFactory;
            _userResponseMapper = userResponseMapper;
            _guidUserNameGenerator = guidUserNameGenerator;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger;
        }

        // Đăng ký tài khoản Local
        public async Task<ResultDTO> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu RegisterUserAsync với user {Email}", registerUser.Email);
            // Validate dữ liệu đăng ký
            await _validationService.ValidateAsync(registerUser, cancellationToken);

            // username mới đăng ký ngãu nhiên
            var userName = _guidUserNameGenerator.GenerateUserName();

            // Tạo đối tượng User mới
            var user = _userFactory.CreateLocalUser(registerUser, userName);

            // Bắt đầu transaction 
            var result = await _userDomainService.RegisterUserAsync(user, registerUser.Password, cancellationToken);

            _logger.LogInformation("Orchestrator: Đăng ký user {Email} thành công!", registerUser.Email);
            return ResultDTO.Success("Đăng ký user mới thành công.");
        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký
        public async Task<ResultDTO<ExternalLoginUserResponseDTO>> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ProviderLoginOrRegisterUserAsync với user {Email} bên {External}", request.Email,request.TypeLogin);

            var result = await _userDomainService.ProviderLoginOrRegisterUserAsync(request, cancellationToken);

            _logger.LogInformation("Orchestrator: Đăng ký, đăng nhập user {Email} bên {External} thành công!", request.Email,request.TypeLogin);
            return ResultDTO<ExternalLoginUserResponseDTO>.Success(_userResponseMapper.ToExternalLogin(result), "Đăng nhập thành công.");

        }
        // Cập nhật thông tin user
        public async Task<ResultDTO<UpdateInfoUserResponseDTO>> UpdateInfoUserAsync(InfoUserUpdateDTO update, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateInfoUserAsync với user {UserName}", update.UserName);

            // Lấy userId từ token
            var userId = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // User mặc định có Claims là rỗng chứ không null.
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Orchestrator: Không xác định được UserId đã gửi update!");
                throw new UserNotFoundByIdException(userId);
            }

            // Validate dữ liệu cập nhật
            await _validationService.ValidateAsync(update, userId, cancellationToken);

            //Update thông tin user
            var result = await _userDomainService.UpdateInfoUserAsync(update, userId, cancellationToken);

            _logger.LogInformation("Orchestrator: Cập nhật thông tin user {UserName} thành công!", update.UserName);
            return ResultDTO<UpdateInfoUserResponseDTO>.Success(_userResponseMapper.ToUpdate(result), "Cập nhật thông tin người dùng thành công.");
        }

        // Login local
        public async Task<ResultDTO<LoginLocalUserResponseDTO>> LoginUserAsync(LoginRequestDTO requestLogin , CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu LoginUserAsync với tài khoản {Account}", requestLogin.Account);
            // Validate dữ liệu đăng nhập
            var user = await _validationService.ValidateAsync<LoginRequestDTO,User>(requestLogin,cancellationToken);

            // Đăng nhập user
            var result = await _userDomainService.LoginUserAsync(user, requestLogin, cancellationToken);

            _logger.LogInformation("Orchestrator: Đăng nhập user {Account} thành công!", requestLogin.Account);
            return ResultDTO<LoginLocalUserResponseDTO>.Success(_userResponseMapper.ToLocalLogin(result), "Đăng nhập thành công.");
        }

        // Tìm user bằng email
        public async Task<ResultDTO<UserResponseDTO>> FindUserByEmailAsync(FindUserByEmailDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindUserByEmailAsync với email {Email}", dto.Email);
            var user = await _userDomainService.FindUserByEmailAsync(dto.Email, cancellationToken);
            _logger.LogInformation("Orchestrator: Tìm thấy user với email {Email}", dto.Email);
            return ResultDTO<UserResponseDTO>.Success(_userResponseMapper.ToDto(user),$"Đã tìm thấy người dùng {dto.Email}");
        }

        // Đặt lại mật khẩu
        public async Task<ResultDTO> ResetPasswordAsync(ResetPasswordRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ResetPasswordAsync với user");
            var result = await _userDomainService.ResetPasswordAsync(dto.UserId,dto.ConfirmNewPassword, cancellationToken);
            _logger.LogInformation("Orchestrator: Reset mật khẩu thành công cho email {Email}", result.Email);
            return ResultDTO.Success("Đặt lại mật khẩu thành công.");
        }
    }
}
