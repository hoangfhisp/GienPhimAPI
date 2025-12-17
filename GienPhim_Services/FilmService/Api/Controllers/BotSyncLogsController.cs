using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotSyncLogsController : ControllerBase
    {
        private readonly IBotSyncLogService _botSyncLogService;
        private readonly ILogger<BotSyncLogsController> _logger;

        public BotSyncLogsController(IBotSyncLogService botSyncLogService, ILogger<BotSyncLogsController> logger)
        {
            _botSyncLogService = botSyncLogService;
            _logger = logger;
        }

        // Lấy lịch sử Bot Sync Logs (phân trang)
        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllBotSyncLogs page={Page}, limit={Limit}", page, limit);

            var result = await _botSyncLogService.GetAllAsync(page, limit, cancellationToken);

            _logger.LogInformation("API Response: GetAllBotSyncLogs Status={Status}", result.Status);

            return Ok(result);
        }
    }
}
