using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.InteractionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly ILogger<ChatMessageController> _logger;

        public ChatMessageController(
            ILogger<ChatMessageController> logger,
            IChatMessageService chatMessageService)
        {
            _logger = logger;
            _chatMessageService = chatMessageService;
        }

        //  Lấy danh sách tin nhắn theo Group
        [HttpGet("ByGroup/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByGroup(Guid groupId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetMessagesByGroup GroupId={GroupId}, Page={Page}, PageSize={PageSize}", groupId, page, pageSize);

            var result = await _chatMessageService.GetMessagesByGroupAsync(groupId, page, pageSize, ct);

            _logger.LogInformation("API Response: GetMessagesByGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        //  Gửi tin nhắn vào phòng chat
        [HttpPost("Send")]
        [Authorize]
        public async Task<IActionResult> Send([FromBody] ChatMessageCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: SendMessage GroupId={GroupId}", dto.GroupId);

            var result = await _chatMessageService.SendMessageAsync(dto, ct);

            _logger.LogInformation("API Response: SendMessage GroupId={GroupId} Status={Status}", dto.GroupId, result.Status);

            return Ok(result);
        }
    }
}
