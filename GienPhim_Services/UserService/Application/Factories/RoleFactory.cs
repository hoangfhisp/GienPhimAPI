using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Factories
{
    public class RoleFactory : IRoleFactory
    {
        // Tạo vai trò mới từ DTO
        public Role CreateRole(RoleCreateDTO roleCreate)
        {
            return new Role
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = roleCreate.RoleName,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }
    }
}
