using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
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
        public async Task<User> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu RegisterUserAsync với user {Email}", registerUser.Email);
            // Validate dữ liệu đăng ký
            await _validationService.ValidateAsync(registerUser, cancellationToken);

            // username mới đăng ký ngãu nhiên
            var userName = _guidUserNameGenerator.GenerateUserName();

            // Tạo đối tượng User mới
            var user = _userFactory.CreateLocalUser(registerUser, userName);

            // Kiểm tra điều kiện đăng ký
            _userDomainService.EnsureCanRegister(user);

            _logger.LogInformation("Orchestrator: Đăng ký user {Email} thành công!", registerUser.Email);
            return user;
        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký   
        public async Task<User> ProviderRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ProviderRegisterUserAsync với user {Email} bên {External}", request.Email, request.TypeLogin);

            await _validationService.ValidateAsync(request, cancellationToken);

            // username mới đăng ký ngãu nhiên
            var userName = _guidUserNameGenerator.GenerateUserName();

            // Tạo đối tượng User mới
            var user = _userFactory.CreateExternalUser(request, userName);

            // Kiểm tra điều kiện đăng ký
            _userDomainService.EnsureCanProviderRegister(user);
            _logger.LogInformation("Orchestrator: Đăng ký user {Email} bên {External} thành công!", request.Email, request.TypeLogin);
            return user;
        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký
        public User ProviderLoginUser(ExternalLoginUserInfoDTO request)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ProviderLoginUserAsync với user {Email} bên {External}", request.Email, request.TypeLogin);

            // Tạo đối tượng User mới
            var user = _userFactory.CreateExternalUser(request, "");

            // Kiểm tra điều kiện đăng nhập
            _userDomainService.EnsureCanProviderLogin(user);
            _logger.LogInformation("Orchestrator: Đăng nhập user {Email} bên {External} thành công!", user.Email, user.TypeLogin);
            return user;
        }

        // Cập nhật thông tin user
        public async Task<User> UpdateInfoUserAsync(InfoUserUpdateDTO update, CancellationToken cancellationToken = default)
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

            // Tạo đối tượng User từ dữ liệu cập nhật
            var user = _userFactory.CreateUpdateInfoUser(update, userId);

            //Update thông tin user
            _userDomainService.EnsureCanUpdateInfoUser(user);

            _logger.LogInformation("Orchestrator: Cập nhật thông tin user {UserName} thành công!", update.UserName);
            return user;
        }

        // Login local
        public async Task<User> LoginLocalUserAsync(LoginRequestDTO requestLogin , CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu LoginUserAsync với tài khoản {Account}", requestLogin.Account);
            // Validate dữ liệu đăng nhập
            var user = await _validationService.ValidateAsync<LoginRequestDTO,User>(requestLogin,cancellationToken);

            // Đăng nhập user
            _userDomainService.EnsureCanLoginLocal(user);

            _logger.LogInformation("Orchestrator: Đăng nhập user {Account} thành công!", requestLogin.Account);
            
            return user;
        }

        // Tìm user bằng email
        public User RepareUserByEmail(FindUserByEmailDTO dto)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindUserByEmailAsync với email {Email}", dto.Email);

            // Tạo đối tượng User từ email
            var user = _userFactory.CreateEmail(dto);

            // Validate email
            _userDomainService.EnsureCanUserEmail(user);
            _logger.LogInformation("Orchestrator: Tìm thấy user với email {Email}", dto.Email);
            return user;
        }

        // Tìm user bằng userId
        public User RepareUserByUserId(FindUserByIdDTO dto)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindUserByIdAsync với userId {UserId}", dto.UserId);
            // Tạo đối tượng User từ userId
            var user = _userFactory.CreateUserId(dto);
            // Validate userId
            _userDomainService.EnsureCanUserId(user);
            _logger.LogInformation("Orchestrator: Tìm thấy user với userId {UserId}", dto.UserId);
            return user;
        }

        // Đặt lại mật khẩu
        public User ResetPasswordAsync(ResetPasswordRequestDTO dto )
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ResetPasswordAsync với user");
            var user = _userFactory.CreateResetPassword(dto);

            _userDomainService.EnsureCanResetPassword(user);
            _logger.LogInformation("Orchestrator: Reset mật khẩu thành công cho email {Email}", user.Id);
            return user;
        }
    }
}
