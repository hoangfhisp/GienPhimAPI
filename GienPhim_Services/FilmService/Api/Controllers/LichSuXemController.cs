using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuXemController : ControllerBase
    {
        private readonly ILichSuXemService _lichSuXemService;
        private readonly ILogger<LichSuXemController> _logger;

        public LichSuXemController(ILichSuXemService lichSuXemService, ILogger<LichSuXemController> logger)
        {
            _lichSuXemService = lichSuXemService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetLichSuXemByUserId")]
        public async Task<IActionResult> GetLichSuXemByUserId([FromQuery] GetLichSuXemUserRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetLichSuXemByUserId UserId={UserId}, page={Page}, limit={Limit}", request.UserId, request.page, request.limit);
            var result = await _lichSuXemService.GetByUserIdAsync(request, cancellationToken);
            _logger.LogInformation("API Response: GetLichSuXemByUserId Status={Status}, Total={Total}", result.Status, result.pagination?.Total);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("XuLyLichSuXem")]
        public async Task<IActionResult> XuLyLichSuXem(LichSuXemCreateDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: XuLyLichSuXem UserId={UserId}, PhimId={PhimId}, TapVuaXem={TapVuaXem}", request.UserId, request.PhimId, request.TapVuaXem);
            var result = await _lichSuXemService.XuLyLichSuXemAsync(request, cancellationToken);
            _logger.LogInformation("API Response: XuLyLichSuXem Status={Status}", result.Status);
            return Ok(result);
        }
    }
}
