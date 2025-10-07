using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Mappers
{
    public interface IRoleResponseMapper
    {
        RoleResponseDTO toDto(Role role); // Chuyển đổi Role thành RoleResponseDTO
    }
}
