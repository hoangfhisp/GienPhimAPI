using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaoDienCuaPhimController : ControllerBase
    {
        private readonly IDaoDienCuaPhimService _service;
        private readonly ILogger<DaoDienCuaPhimController> _logger;

        public DaoDienCuaPhimController(IDaoDienCuaPhimService service, ILogger<DaoDienCuaPhimController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // Lấy danh sách quan hệ Đạo diễn - Phim (có phân trang)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAll DaoDienCuaPhim page={Page}, limit={Limit}", page, limit);

            var result = await _service.GetAllAsync(page, limit, cancellationToken);

            _logger.LogInformation("API Response: GetAll DaoDienCuaPhim Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        // Tạo quan hệ mới
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DaoDienCuaPhimCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create DaoDienCuaPhim PhimId={PhimId}, DaoDienId={DaoDienId}", dto.PhimId, dto.DaoDienId);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create DaoDienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Cập nhật quan hệ
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] DaoDienCuaPhimUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update DaoDienCuaPhim PhimId={PhimId}, DaoDienId={DaoDienId}", dto.PhimId, dto.DaoDienId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update DaoDienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Xóa quan hệ
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DaoDienCuaPhimDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete DaoDienCuaPhim PhimId={PhimId}, DaoDienId={DaoDienId}", dto.PhimId, dto.DaoDienId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete DaoDienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }
    }
}
