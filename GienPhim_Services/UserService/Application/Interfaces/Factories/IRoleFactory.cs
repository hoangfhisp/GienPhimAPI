using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Factories
{
    public interface IRoleFactory
    {
        Role CreateRole(RoleCreateDTO roleCreate); // Tạo vai trò mới từ DTO
    }
}
