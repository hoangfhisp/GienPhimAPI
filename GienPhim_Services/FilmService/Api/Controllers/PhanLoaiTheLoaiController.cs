using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaiTheLoaiController : ControllerBase
    {
        private readonly IPhanLoaiTheLoaiService _service;
        private readonly ILogger<PhanLoaiTheLoaiController> _logger;

        public PhanLoaiTheLoaiController(IPhanLoaiTheLoaiService service, ILogger<PhanLoaiTheLoaiController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int limit = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetAll PhanLoaiTheLoai page={Page}, limit={Limit}", page, limit);

            var result = await _service.GetAllAsync(page, limit, ct);

            _logger.LogInformation("API Response: GetAll PhanLoaiTheLoai Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PhanLoaiTheLoaiCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", dto.PhimId, dto.TheLoaiId);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create PhanLoaiTheLoai Status={Status}", result.Status);

            return Ok(result);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] PhanLoaiTheLoaiUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", dto.PhimId, dto.TheLoaiId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId} Status={Status}", dto.PhimId, dto.TheLoaiId, result.Status);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] PhanLoaiTheLoaiDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", dto.PhimId, dto.TheLoaiId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId} Status={Status}", dto.PhimId, dto.TheLoaiId, result.Status);

            return Ok(result);
        }
    }
}
