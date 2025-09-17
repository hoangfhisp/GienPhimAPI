using GienPhim_Services.UserService.Application.DTOs.Admin.Role;
using GienPhim_Services.UserService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        // Tạo Role mới
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleCreateDTO createRole, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: Bắt đầu CreateRole với role {RoleName}", createRole.RoleName);
            var result = await _roleService.CreateRoleAsync(createRole);
            _logger.LogInformation("API Response: Tạo Role {RoleName} với trạng thái {Status}", createRole.RoleName, result.Status);
            return Ok(result);
        }
    }
}
