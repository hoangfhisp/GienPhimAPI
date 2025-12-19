using GienPhim_Services.UserService.Application.DTOs.Admin.User;
using GienPhim_Services.UserService.Application.Exceptions.Invalid;
using GienPhim_Services.UserService.Application.Exceptions.NotFounds;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;

namespace GienPhim_Services.UserService.Application.Validators.Users
{
    public class GetListUserValidator : IValidator<ListUserRequestDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetListUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ListUserRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var userList = await _userRepository.GetAllUsersAsync(dto.page,dto.limit,cancellationToken);

            if(userList.total == 0)
            {
                throw new UserListNotFoundException();
            }

            if (userList.last < dto.page)
            {
                throw new InvalidPageIndexOutOfRangeException(dto.page, userList.total);
            }
        }
    }
}
