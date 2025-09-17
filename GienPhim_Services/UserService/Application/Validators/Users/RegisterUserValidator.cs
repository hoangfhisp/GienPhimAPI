using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.Exceptions.Duplicates;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;

namespace GienPhim_Services.UserService.Application.Validators.Users
{
    public class RegisterUserValidator : IValidator<UserRegisterDTO>

    {
        private readonly IUserRepository _userRepository;

        public RegisterUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // Kiểm tra trùng email, sđt khi đăng ký
        public async Task ValidateAsync(UserRegisterDTO dto, CancellationToken cancellationToken = default)
        {
            var existingByEmail = await _userRepository.FindByEmailAsync(dto.Email,cancellationToken);
            if (existingByEmail != null)
                throw new DuplicateEmailException(dto.Email);

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var existingByPhone = await _userRepository.FindByPhoneNumberAsync(dto.PhoneNumber, cancellationToken);
                if (existingByPhone != null)
                    throw new DuplicatePhoneNumberException(dto.PhoneNumber);
            }
        }
    }
}
