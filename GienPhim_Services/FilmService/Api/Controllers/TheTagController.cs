using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheTagController : ControllerBase
    {
        private readonly ITheTagService _theTagService;
        private readonly ILogger<TheTagController> _logger;

        public TheTagController(
            ILogger<TheTagController> logger,
            ITheTagService theTagService)
        {
            _theTagService = theTagService;
            _logger = logger;
        }

        // Lấy danh sách thẻ tag
        [HttpGet("GetAllTheTags")]
        public async Task<IActionResult> GetAllTheTags(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllTheTagsAsync");
            var result = await _theTagService.GetAllTheTagsAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllTheTagsAsync Status={Status}", result.Status);
            return Ok(result);
        }

        // Tạo mới thẻ tag
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TheTagCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateTheTag với tên {TenT}", dto.TenT);
            var result = await _theTagService.CreateTheTagAsync(dto, ct);
            _logger.LogInformation("API Response: CreateTheTag Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật thẻ tag
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] TheTagUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateTheTag Id={TheTagId}", dto.TheTagId);
            var result = await _theTagService.UpdateTheTagAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateTheTag Id={TheTagId} Status={Status}", dto.TheTagId, result.Status);
            return Ok(result);
        }

        // Xóa thẻ tag
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] TheTagDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteTheTag Id={TheTagId}", deleteDto.TheTagId);
            var result = await _theTagService.DeleteTheTagAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteTheTag Id={TheTagId} Status={Status}", deleteDto.TheTagId, result.Status);
            return Ok(result);
        }
    }
}
