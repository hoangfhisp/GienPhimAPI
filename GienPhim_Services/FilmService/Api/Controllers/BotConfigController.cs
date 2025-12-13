using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotConfigController : ControllerBase
    {
        private readonly IBotConfigService _botConfigService;
        private readonly ILogger<BotConfigController> _logger;

        public BotConfigController(IBotConfigService botConfigService, ILogger<BotConfigController> logger)
        {
            _botConfigService = botConfigService;
            _logger = logger;
        }

        // Lấy cấu hình Bot
        [HttpGet("GetConfig")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetConfig(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetBotConfig");

            var result = await _botConfigService.GetBotConfigAsync(cancellationToken);

            _logger.LogInformation("API Response: GetBotConfig Status={Status}", result.Status);

            return Ok(result);
        }

        // Cập nhật cấu hình Bot
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] BotConfigUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: UpdateBotConfig với dữ liệu {@Dto}", dto);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("API Warning: Dữ liệu cập nhật BotConfig không hợp lệ");
                return BadRequest(ModelState);
            }

            var result = await _botConfigService.UpdateBotConfigAsync(dto, cancellationToken);

            _logger.LogInformation("API Response: UpdateBotConfig Status={Status}", result.Status);

            return Ok(result);
        }
    }
}
