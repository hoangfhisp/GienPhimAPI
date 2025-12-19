using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ResultDTO> CreateRoleAsync(RoleCreateDTO createRole, CancellationToken cancellationToken = default); // Tạo Role
        Task<ResultListDTO<RoleResponseDTO>> GetAllRoleAsync(GetListRoleRequestDTO request, CancellationToken cancellationToken = default); // Lấy tất cả Role
        Task<ResultListDTO<RoleResponseDTO>> GetRoleByRoleNameAsync(FindRoleByRoleNameRequestDTO request, CancellationToken cancellationToken = default); // Tìm Role theo tên
    }
}
