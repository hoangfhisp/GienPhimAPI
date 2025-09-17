using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Services;
using GienPhim_Services.UserService.Application.Interfaces.Validators;

namespace GienPhim_Services.UserService.Application.Services
{
    public class RoleService : IRoleService
    {
        public readonly IRoleOrchestrator _roleOrchestrator;

        public RoleService(IRoleOrchestrator roleOrchestrator)
        {
            _roleOrchestrator = roleOrchestrator;
        }

        //Role
        //Tạo Role mới
        public async Task<ResultDTO> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default)
        {
            return await _roleOrchestrator.CreateRoleAsync(createRole, cancellationToken);
        }
    }
}
