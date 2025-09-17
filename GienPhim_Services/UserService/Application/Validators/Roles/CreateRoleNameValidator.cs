using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.Exceptions.Duplicates;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;

namespace GienPhim_Services.UserService.Application.Validators.Roles
{
    public class CreateRoleNameValidator : IValidator<RoleCreateDTO>
    {
        private readonly IRoleRepository _roleRepository;
        public CreateRoleNameValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task ValidateAsync(RoleCreateDTO roleCreate, CancellationToken cancellationToken = default)
        {
            var existingRole = await _roleRepository.FindByRoleNameAsync(roleCreate.RoleName);
            if (existingRole != null)
                throw new DuplicateRoleNameException(roleCreate.RoleName);
        }
    }
}
