using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DienVienCuaPhimController : ControllerBase
    {
        private readonly IDienVienCuaPhimService _service;
        private readonly ILogger<DienVienCuaPhimController> _logger;

        public DienVienCuaPhimController(IDienVienCuaPhimService service, ILogger<DienVienCuaPhimController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // Lấy danh sách quan hệ Diễn viên - Phim (có phân trang)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAll DienVienCuaPhim page={Page}, limit={Limit}", page, limit);

            var result = await _service.GetAllAsync(page, limit, cancellationToken);

            _logger.LogInformation("API Response: GetAll DienVienCuaPhim Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        // Tạo quan hệ mới
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DienVienCuaPhimCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create DienVienCuaPhim PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create DienVienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Cập nhật quan hệ
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] DienVienCuaPhimUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update DienVienCuaPhim PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update DienVienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Xóa quan hệ
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DienVienCuaPhimDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete DienVienCuaPhim PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete DienVienCuaPhim Status={Status}", result.Status);

            return Ok(result);
        }
    }
}
