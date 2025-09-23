using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Entities;
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
        public async Task<Role> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateRoleAsync với role {RoleName}", createRole.RoleName);
            // Check Role đã tồn tại chưa
            await _validationService.ValidateAsync(createRole,cancellationToken);

            // Tạo đối tượng Role mới
            var role = _roleFactory.CreateRole(createRole);

            _roleDomainService.EnsureCanCreate(role); // Kiểm tra các điều kiện để tạo Role mới

            _logger.LogInformation("[Orchestrator] Role '{RoleName}' đã được validate và khởi tạo thành công", createRole.RoleName);
            return role;
        }
    }
}
