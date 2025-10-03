using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TapPhimController : ControllerBase
    {
        private readonly ITapPhimService _service;
        private readonly ILogger<TapPhimController> _logger;

        public TapPhimController(ITapPhimService service, ILogger<TapPhimController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // Lấy danh sách tất cả tập phim (phân trang)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int limit = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetAll TapPhim page={Page}, limit={Limit}", page, limit);

            var result = await _service.GetAllAsync(page, limit, ct);

            _logger.LogInformation("API Response: GetAll TapPhim Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        // Lấy danh sách tập theo PhimId
        [HttpGet("GetByPhimId")]
        public async Task<IActionResult> GetByPhimId(int phimId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetByPhimId phimId={PhimId}", phimId);

            var result = await _service.GetByPhimIdAsync(phimId, ct);

            _logger.LogInformation("API Response: GetByPhimId phimId={PhimId} Status={Status}", phimId, result.Status);

            return Ok(result);
        }

        // Tạo tập phim
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] TapPhimCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create TapPhim PhimId={PhimId}, TenTap={TenTap}", dto.PhimId, dto.TenTap);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create TapPhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Cập nhật tập phim
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] TapPhimUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update TapPhim TapId={TapId}, PhimId={PhimId}", dto.TapId, dto.PhimId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update TapPhim TapId={TapId} Status={Status}", dto.TapId, result.Status);

            return Ok(result);
        }

        // Xóa tập phim
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] TapPhimDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete TapPhim TapId={TapId}", dto.TapId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete TapPhim TapId={TapId} Status={Status}", dto.TapId, result.Status);

            return Ok(result);
        }
    }
}
