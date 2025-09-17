using Azure.Core;
using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.Exceptions.Duplicates;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;

namespace GienPhim_Services.UserService.Application.Validators.Users
{
    public class ProviderRegisterUserValidator : IValidator<ExternalLoginUserInfoDTO>
    {
        private readonly IUserRepository _userRepository;

        public ProviderRegisterUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ExternalLoginUserInfoDTO externalLoginUserInfo, CancellationToken cancellationToken = default)
        {
            // Kiểm tra trùng email
            var emailExist = await _userRepository.FindByEmailAsync(externalLoginUserInfo.Email,cancellationToken);
            if (emailExist != null)
                throw new DuplicateEmailException(externalLoginUserInfo.Email);
        }
    }
}
