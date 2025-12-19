using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.Exceptions.NotFounds;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Validators.Users
{
    public class LocalLoginUserValidator : IEntityValidator<LoginRequestDTO,User>
    {
        private readonly IUserRepository _userRepository;

        public LocalLoginUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task ValidateAsync(LoginRequestDTO updateInfo, CancellationToken cancellationToken = default)
        {

        }
        public async Task<User> ValidateAndGetAsync(LoginRequestDTO loginRequest, CancellationToken cancellationToken = default)
        {
            // Check tài khoản tồn tại chưa (email hoặc username)
            var checkAccount = await _userRepository.FindByEmailAsync(loginRequest.Account, cancellationToken)
                    ?? await _userRepository.FindByUsernameAsync(loginRequest.Account, cancellationToken);

            if (checkAccount == null)
            {
                throw new UserNotFoundException(loginRequest.Account);
            }

            return checkAccount;
        }
    }
}
