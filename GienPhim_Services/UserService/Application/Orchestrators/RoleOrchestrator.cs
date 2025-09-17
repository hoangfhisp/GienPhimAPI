using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Services.Interfaces;

namespace GienPhim_Services.UserService.Application.Orchestrators
{
    public class RoleOrchestrator : IRoleOrchestrator
    {
        private readonly IRoleFactory _roleFactory;
        private readonly IValidationService _validationService;
        private readonly IRoleDomainService _roleDomainService;
        private readonly ILogger<RoleOrchestrator> _logger;

        public RoleOrchestrator(IRoleFactory roleFactory, IValidationService validationService
            , IRoleDomainService roleDomainService, ILogger<RoleOrchestrator> logger)
        {
            _roleFactory = roleFactory;
            _validationService = validationService;
            _roleDomainService = roleDomainService;
            _logger = logger;
        }

        //Tạo Role mới
        public async Task<ResultDTO> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateRoleAsync với role {RoleName}", createRole.RoleName);
            // Check Role đã tồn tại chưa
            await _validationService.ValidateAsync(createRole,cancellationToken);

            // Tạo đối tượng Role mới
            var role = _roleFactory.CreateRole(createRole);

            // Tạo Role mới
            var result = await _roleDomainService.CreateRoleAsync(role,cancellationToken);

            _logger.LogInformation("Orchestrator: Tạo Role {RoleName} thành công!", createRole.RoleName);
            return ResultDTO.Success($"Tạo Role '{role.RoleName}' mới thành công.");
        }
    }
}
