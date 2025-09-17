// using thư viện
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Application.Interfaces.Services;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;

namespace GienPhim_Services.UserService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserOrchestrator _userOrchestrator;
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository
            , IUserOrchestrator userOrchestrator)
        {
            //tham số của phương thức bị null nhưng không được phép null.
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userOrchestrator = userOrchestrator ?? throw new ArgumentNullException(nameof(userOrchestrator));
        }

        // USER
        // Đăng ký tài khoản Local
        public async Task<ResultDTO> RegisterUserAsync(UserRegisterDTO registerUser, CancellationToken cancellationToken = default)
        {
            // Bắt đầu transaction 
            return await _userOrchestrator.RegisterUserAsync(registerUser, cancellationToken);

        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký
        public async Task<ResultDTO<ExternalLoginUserResponseDTO>> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request ,CancellationToken cancellationToken = default)
        {
            return await _userOrchestrator.ProviderLoginOrRegisterUserAsync(request, cancellationToken);

        }


        // Update Info người dùng
        public async Task<ResultDTO<UpdateInfoUserResponseDTO>> UpdateInfoUserAsync(InfoUserUpdateDTO update,CancellationToken cancellationToken = default)
        {

            return await _userOrchestrator.UpdateInfoUserAsync(update, cancellationToken);

        }


        // Login và sinh ra token
        public async Task<ResultDTO<LoginLocalUserResponseDTO>> LoginUserAsync(LoginRequestDTO requestLogin,CancellationToken cancellationToken = default)
        {
            return await _userOrchestrator.LoginUserAsync(requestLogin,cancellationToken);
        }

        // Tìm User theo email
        public async Task<ResultDTO<UserResponseDTO>> FindUserByEmailAsync(FindUserByEmailDTO dto, CancellationToken cancellationToken = default)
        {
            return await _userOrchestrator.FindUserByEmailAsync(dto, cancellationToken);
        }

        // Reset mật khẩu
        public async Task<ResultDTO> ResetPasswordAsync(ResetPasswordRequestDTO dto, CancellationToken cancellationToken = default)
        {
            return await _userOrchestrator.ResetPasswordAsync(dto, cancellationToken);
        }
    }
}
