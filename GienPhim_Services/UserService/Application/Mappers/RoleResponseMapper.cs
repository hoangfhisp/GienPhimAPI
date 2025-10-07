using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.Interfaces.Mappers;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Mappers
{
    public class RoleResponseMapper  : IRoleResponseMapper
    {
        // Chuyển đổi từ Role entity sang RoleResponseDTO
        public RoleResponseDTO toDto (Role role)
        {
            return new RoleResponseDTO
            {
                RoleId = role.Id,
                RoleName = role.Name,
                ThoiGianTao = role.ThoiGianTao,
                ThoiGianCapNhat = role.ThoiGianCapNhat
            };
        }
    }
}
