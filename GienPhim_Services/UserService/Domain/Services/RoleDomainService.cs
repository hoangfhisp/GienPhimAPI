
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Exceptions.Failed;
using GienPhim_Services.UserService.Domain.Services.Interfaces;
using GienPhim_Services.UserService.Shared.Kernel;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Domain.Services
{
    public class RoleDomainService : IRoleDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoleDomainService> _logger;
        public RoleDomainService(IUnitOfWork unitOfWork, ILogger<RoleDomainService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Tạo Role mới
        public async Task<IdentityResult> CreateRoleAsync(Role role, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainService: Bắt đầu CreateRoleAsync với role {RoleName}", role.RoleName);
            // Bắt đầu transaction
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tạo Role mới
            var result = await _unitOfWork.Roles.CreateRoleAsync(role);
            if (!result.Succeeded)
            {
                _logger.LogError("DomainService: Tạo Role {RoleName} thất bại!", role.RoleName);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateRoleFailedException(role.RoleName);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("DomainService: Tạo Role {RoleName} thành công!", role.RoleName);
            return result;

        }
    }
}
