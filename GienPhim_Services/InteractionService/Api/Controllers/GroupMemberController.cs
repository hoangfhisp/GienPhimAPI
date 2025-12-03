using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.InteractionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly IGroupMemberService _groupMemberService;
        private readonly ILogger<GroupMemberController> _logger;

        public GroupMemberController(
            ILogger<GroupMemberController> logger,
            IGroupMemberService groupMemberService)
        {
            _logger = logger;
            _groupMemberService = groupMemberService;
        }

        // Lấy danh sách thành viên của 1 phòng chat
        [HttpGet("ByGroup/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetMembersByGroup(Guid groupId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetMembersByGroup GroupId={GroupId}, Page={Page}, PageSize={PageSize}", groupId, page, pageSize);

            var result = await _groupMemberService.GetMembersByGroupAsync(groupId, page, pageSize, ct);

            _logger.LogInformation("API Response: GetMembersByGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // User tham gia phòng chat
        [HttpPost("Join")]
        [Authorize]
        public async Task<IActionResult> Join([FromBody] GroupMemberJoinDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: JoinGroup GroupId={GroupId}", dto.GroupId);

            var result = await _groupMemberService.JoinGroupAsync(dto, ct);

            _logger.LogInformation("API Response: JoinGroup GroupId={GroupId} Status={Status}", dto.GroupId, result.Status);

            return Ok(result);
        }

        // User rời phòng chat
        [HttpPost("Leave/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> Leave(Guid groupId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: LeaveGroup GroupId={GroupId}", groupId);

            var result = await _groupMemberService.LeaveGroupAsync(groupId, ct);

            _logger.LogInformation("API Response: LeaveGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // Cập nhật quyền của 1 thành viên (mute, canChat, canControlFilm)
        [HttpPut("UpdatePermissions/{groupId:guid}/{targetUserId}")]
        [Authorize]
        public async Task<IActionResult> UpdatePermissions(Guid groupId, string targetUserId, [FromBody] GroupMemberUpdatePermissionsDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdatePermissions GroupId={GroupId}, TargetUser={TargetUser}", groupId, targetUserId);

            var result = await _groupMemberService.UpdatePermissionsAsync(groupId, targetUserId, dto, ct);

            _logger.LogInformation("API Response: UpdatePermissions GroupId={GroupId}, TargetUser={TargetUser} Status={Status}", groupId, targetUserId, result.Status);

            return Ok(result);
        }

        // Chuyển quyền chủ phòng
        [HttpPost("TransferOwner/{groupId:guid}/{newOwnerUserId}")]
        [Authorize]
        public async Task<IActionResult> TransferOwner(Guid groupId, string newOwnerUserId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: TransferOwnership GroupId={GroupId}, NewOwner={NewOwner}", groupId, newOwnerUserId);

            var result = await _groupMemberService.TransferOwnershipAsync(groupId, newOwnerUserId, ct);

            _logger.LogInformation("API Response: TransferOwnership GroupId={GroupId}, NewOwner={NewOwner} Status={Status}", groupId, newOwnerUserId, result.Status);

            return Ok(result);
        }
    }
}
