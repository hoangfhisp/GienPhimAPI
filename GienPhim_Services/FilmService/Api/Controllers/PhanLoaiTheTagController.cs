using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaiTheTagController : ControllerBase
    {
        private readonly IPhanLoaiTheTagService _service;
        private readonly ILogger<PhanLoaiTheTagController> _logger;

        public PhanLoaiTheTagController(IPhanLoaiTheTagService service, ILogger<PhanLoaiTheTagController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int limit = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetAll PhanLoaiTheTag page={Page}, limit={Limit}", page, limit);

            var result = await _service.GetAllAsync(page, limit, ct);

            _logger.LogInformation("API Response: GetAll PhanLoaiTheTag Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PhanLoaiTheTagCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create PhanLoaiTheTag PhimId={PhimId}, TheTagId={TheTagId}", dto.PhimId, dto.TheTagId);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create PhanLoaiTheTag Status={Status}", result.Status);

            return Ok(result);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] PhanLoaiTheTagUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update PhanLoaiTheTag PhimId={PhimId}, TheTagId={TheTagId}", dto.PhimId, dto.TheTagId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update PhanLoaiTheTag PhimId={PhimId}, TheTagId={TheTagId} Status={Status}", dto.PhimId, dto.TheTagId, result.Status);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] PhanLoaiTheTagDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete PhanLoaiTheTag PhimId={PhimId}, TheTagId={TheTagId}", dto.PhimId, dto.TheTagId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete PhanLoaiTheTag PhimId={PhimId}, TheTagId={TheTagId} Status={Status}", dto.PhimId, dto.TheTagId, result.Status);

            return Ok(result);
        }
    }
}
