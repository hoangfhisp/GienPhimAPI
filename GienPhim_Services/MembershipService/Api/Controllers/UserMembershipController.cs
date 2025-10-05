using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.MembershipService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembershipController : ControllerBase
    {
        private readonly IUserMembershipService _userMembershipService;
        private readonly ILogger<UserMembershipController> _logger;

        public UserMembershipController(
            ILogger<UserMembershipController> logger,
            IUserMembershipService userMembershipService)
        {
            _userMembershipService = userMembershipService;
            _logger = logger;
        }

        // Lấy danh sách UserMembership (có phân trang)
        [HttpGet("GetAllUserMemberships")]
        public async Task<IActionResult> GetAllUserMemberships(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllUserMembershipsAsync");
            var result = await _userMembershipService.GetAllUserMembershipsAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllUserMembershipsAsync với trạng thái {Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết UserMembership theo Id
        [HttpGet("Detail/{userMembershipId}")]
        public async Task<IActionResult> GetDetail(string userMembershipId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail UserMembership Id={UserMembershipId}", userMembershipId);
            var result = await _userMembershipService.GetDetailAsync(userMembershipId, ct);
            return Ok(result);
        }

        // Tạo mới UserMembership
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserMembershipCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create UserMembership cho UserId={UserId}, OptionId={OptionId}", dto.UserId, dto.OptionId);
            var result = await _userMembershipService.CreateUserMembershipAsync(dto, ct);
            _logger.LogInformation("API Response: Create UserMembership UserId={UserId} Status={Status}", dto.UserId, result.Status);
            return Ok(result);
        }

        // Cập nhật UserMembership
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UserMembershipUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update UserMembership Id={UserMembershipId}", dto.UserMembershipId);
            var result = await _userMembershipService.UpdateUserMembershipAsync(dto, ct);
            _logger.LogInformation("API Response: Update UserMembership Id={UserMembershipId} Status={Status}", dto.UserMembershipId, result.Status);
            return Ok(result);
        }

        // Xóa UserMembership
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] UserMembershipDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete UserMembership Id={UserMembershipId}", deleteDto.UserMembershipId);
            var result = await _userMembershipService.DeleteUserMembershipAsync(deleteDto, ct);
            _logger.LogInformation("API Response: Delete UserMembership Id={UserMembershipId} Status={Status}", deleteDto.UserMembershipId, result.Status);
            return Ok(result);
        }
    }
}