using GienPhim_Services.UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreateRoleAsync(Role role); // Tạo Role mới
        Task<Role> FindByRoleNameAsync(string roleName); // Lấy Role Từ RoleName
    }
}
