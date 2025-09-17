using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Shared.Kernel;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Domain.Services.Interfaces
{
    public interface IRoleDomainService
    {
        Task<IdentityResult> CreateRoleAsync(Role role, CancellationToken cancellationToken = default); // Tạo Role mới
    }
}
