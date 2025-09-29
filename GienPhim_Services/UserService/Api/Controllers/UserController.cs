using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Đăng ký
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserRegisterDTO registerUser,CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Request: RegisterUser cho email {Email}", registerUser.Email);

            var result = await _userService.RegisterUserAsync(registerUser, cancellationToken);
            _logger.LogInformation("API Response: RegisterUser cho email {Email} với trạng thái {Status}",registerUser.Email, result.Status);
            return Ok(result);
        }

        // Update Thông tin người dùng
        [HttpPut("UpdateInfoUser")]
        [Authorize]
        public async Task<IActionResult> UpdateInfoUser([FromForm]InfoUserUpdateDTO update, CancellationToken cancellationToken) //[FromForm] bắt buộc server lấy dữ liệu từ form-data, gồm cả IFormFile.
        {
            _logger.LogInformation("API Request: UpdateInfoUser cho user {UserName}", update.UserName);

            var result = await _userService.UpdateInfoUserAsync(update, cancellationToken);
            _logger.LogInformation("API Response: UpdateInfoUser cho user {UserName} với trạng thái {Status}", update.UserName, result.Status);
            return Ok(result);
        }

        // Đăng nhập không jwt
        [HttpPost("LoginLocalUser")]
        public async Task<IActionResult> LoginLocalUser( LoginRequestDTO requestLogin)
        {
            _logger.LogInformation("API Request: LoginUser Local cho tài khoản {Account}", requestLogin.Account);

            var result = await _userService.LoginLocalUserAsync(requestLogin);
            _logger.LogInformation("API Response: LoginUser Local cho tài khoản {Account} với trạng thái {Status}", requestLogin.Account, result.Status);
            return Ok(result);
        }

        // Đăng nhập hoặc đăng ký User bằng Google
        [HttpPost("ProviderLoginOrRegisterUser")]
        public async Task<IActionResult> ProviderLoginOrRegisterUser(ExternalLoginUserInfoDTO request)
        {
            _logger.LogInformation("API Request: ProviderLoginOrRegisterUser cho email {Email}", request.Email);

            var result = await _userService.ProviderLoginOrRegisterUserAsync(request);
            _logger.LogInformation("API Response: ProviderLoginOrRegisterUser cho email {Email} với trạng thái {Status}", request.Email, result.Status);
            return Ok(result);
        }

        // Tìm User theo email
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery]FindUserByEmailDTO dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Request: FindUserByEmail cho email {Email}", dto.Email);
            var result = await _userService.FindUserByEmailAsync(dto, cancellationToken);
            _logger.LogInformation("API Response: FindUserByEmail cho email {Email} với trạng thái {Status}", dto.Email, result.Status);
            return Ok(result);
        }

        // Tìm User theo email
        [HttpGet("GetUserByUserId")]
        public async Task<IActionResult> GetUserByUserId([FromQuery] FindUserByIdDTO dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Request: FindUserByUserId cho id {id}", dto.UserId);
            var result = await _userService.FindUserByUserIdAsync(dto, cancellationToken);
            _logger.LogInformation("API Response: FindUserByUserId cho email {Email} với trạng thái {Status}", result.Data.Email, result.Status);
            return Ok(result);
        }

        // Reset mật khẩu
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDTO dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Request: ResetPassword cho UserId: {userId}",dto.UserId);
            var result = await _userService.ResetPasswordAsync(dto, cancellationToken);
            _logger.LogInformation("API Response: ResetPassword cho UserId {userId} với trạng thái {Status}", dto.UserId, result.Status);
            return Ok(result);
        }

        // Đổi email
        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequestDTO dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Request: ChangeEmail cho UserId: {userId}", dto.UserId);
            var result = await _userService.ChangeEmailAsync(dto, cancellationToken);
            _logger.LogInformation("API Response: ChangeEmail cho UserId {userId} với trạng thái {Status}", dto.UserId, result.Status);
            return Ok(result);
        }
    }
}
