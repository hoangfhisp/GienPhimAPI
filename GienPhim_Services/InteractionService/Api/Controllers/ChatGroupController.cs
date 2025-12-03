using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.InteractionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGroupController : ControllerBase
    {
        private readonly IChatGroupService _chatGroupService;
        private readonly ILogger<ChatGroupController> _logger;

        public ChatGroupController(
            ILogger<ChatGroupController> logger,
            IChatGroupService chatGroupService)
        {
            _logger = logger;
            _chatGroupService = chatGroupService;
        }


        // Lấy chi tiết phòng chat theo GroupId
        [HttpGet("Detail/{groupId:guid}")]
        public async Task<IActionResult> GetDetail(Guid groupId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail ChatGroup GroupId={GroupId}", groupId);

            var result = await _chatGroupService.GetDetailAsync(groupId, ct);

            _logger.LogInformation("API Response: GetDetail ChatGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // Lấy danh sách phòng chat theo phim
        [HttpGet("ByFilm/{phimId:int}")]
        public async Task<IActionResult> GetByFilm(int phimId, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetByFilm ChatGroup PhimId={PhimId}, Status={Status}, Page={Page}, PageSize={PageSize}", phimId, status, page, pageSize);

            var result = await _chatGroupService.GetByFilmAsync(phimId, status, page, pageSize, ct);

            _logger.LogInformation("API Response: GetByFilm ChatGroup PhimId={PhimId} Status={Status}", phimId, result.Status);

            return Ok(result);
        }

        // Lấy danh sách thành viên phòng chat của tôi (owner / member), (oepn / close)
        [HttpGet("Mine")]
        [Authorize]
        public async Task<IActionResult> GetMine([FromQuery] string? role, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetMine ChatGroup Role={Role}, Status={Status}, Page={Page}, PageSize={PageSize}", role, status, page, pageSize);

            var result = await _chatGroupService.GetMineAsync(role, status, page, pageSize, ct);

            _logger.LogInformation("API Response: GetMine ChatGroup Status={Status}", result.Status);

            return Ok(result);
        }

        // Tạo mới phòng chat
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ChatGroupCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateChatGroup PhimId={PhimId}", dto.PhimId);

            var result = await _chatGroupService.CreateChatGroupAsync(dto, ct);

            _logger.LogInformation("API Response: CreateChatGroup PhimId={PhimId} Status={Status}", dto.PhimId, result.Status);

            return Ok(result);
        }

        // Cập nhật cài đặt phòng chat
        [HttpPut("UpdateSettings/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateSettings(Guid groupId, [FromBody] ChatGroupUpdateSettingsDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateSettings ChatGroup GroupId={GroupId}", groupId);

            var result = await _chatGroupService.UpdateSettingsAsync(groupId, dto, ct);

            _logger.LogInformation("API Response: UpdateSettings ChatGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // Làm mới mã mời
        [HttpPost("RefreshInviteCode/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> RefreshInviteCode(Guid groupId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: RefreshInviteCode ChatGroup GroupId={GroupId}", groupId);

            var result = await _chatGroupService.RefreshInviteCodeAsync(groupId, ct);

            _logger.LogInformation( "API Response: RefreshInviteCode ChatGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // Tắt mã mời
        [HttpPost("DisableInviteCode/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> DisableInviteCode(Guid groupId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DisableInviteCode ChatGroup GroupId={GroupId}", groupId);

            var result = await _chatGroupService.DisableInviteCodeAsync(groupId, ct);

            _logger.LogInformation("API Response: DisableInviteCode ChatGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        // Đóng phòng chat
        [HttpPost("Close/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> Close(Guid groupId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CloseChatGroup GroupId={GroupId}", groupId);

            var result = await _chatGroupService.CloseChatGroupAsync(groupId, ct);

            _logger.LogInformation( "API Response: CloseChatGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }
    }
}
