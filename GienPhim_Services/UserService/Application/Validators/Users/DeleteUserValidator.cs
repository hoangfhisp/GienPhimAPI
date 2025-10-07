using GienPhim_Services.UserService.Application.DTOs.Admin.User;
using GienPhim_Services.UserService.Application.Exceptions.NotFounds;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Validators.Users
{
    public class DeleteUserValidator : IEntityValidator<DeleteUserRequestDTO, User>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(DeleteUserRequestDTO request, CancellationToken cancellationToken = default)
        {
           
        }

        public async Task<User> ValidateAndGetAsync(DeleteUserRequestDTO request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByUserIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new UserNotFoundByIdException(request.UserId);

            return user;
        }
    }
}
