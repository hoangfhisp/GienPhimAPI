using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.Exceptions.Failed;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Services;

namespace GienPhim_Services.UserService.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleOrchestrator _roleOrchestrator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoleService> _logger;
        public RoleService(IUnitOfWork unitOfWork, IRoleOrchestrator roleOrchestrator, ILogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _roleOrchestrator = roleOrchestrator;
            _logger = logger;
        }

        //Role
        //Tạo Role mới
        public async Task<ResultDTO> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default)
        {
            // Tạo Role mới
            _logger.LogInformation("ApplicationService: Bắt đầu CreateRoleAsync với role {RoleName}", createRole.RoleName);

            var role = await _roleOrchestrator.CreateRoleAsync(createRole, cancellationToken);

            // Bắt đầu transaction
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tạo Role mới
            var result = await _unitOfWork.Roles.CreateRoleAsync(role);
            if (!result.Succeeded)
            {
                _logger.LogError("ApplicationService: Tạo Role {RoleName} thất bại!", role.RoleName);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateRoleFailedException(role.RoleName);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo Role {RoleName} thành công!", role.RoleName);
            return ResultDTO.Success($"Tạo role {role.RoleName} mới thành công");
        }
    }
}
