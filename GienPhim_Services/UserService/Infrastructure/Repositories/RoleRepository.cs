using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        // Tao Role Mới
        public async Task<IdentityResult> CreateRoleAsync(Role role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        // Lấy Role Từ RoleName
        public async Task<Role> FindByRoleNameAsync(string roleName)
        {
            var result = await _roleManager.FindByNameAsync(roleName);
            return result;
        }
    }
}
