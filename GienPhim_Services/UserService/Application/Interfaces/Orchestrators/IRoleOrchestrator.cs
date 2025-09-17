using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.Interfaces.Orchestrators
{
    public interface IRoleOrchestrator
    {
        Task<ResultDTO> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default); // Tạo Role mới
    }
}
