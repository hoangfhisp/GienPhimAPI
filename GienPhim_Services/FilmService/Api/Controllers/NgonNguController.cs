using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgonNguController : ControllerBase
    {
        private readonly INgonNguService _ngonNguService;
        private readonly ILogger<NgonNguController> _logger;

        public NgonNguController(
            ILogger<NgonNguController> logger,
            INgonNguService ngonNguService)
        {
            _ngonNguService = ngonNguService;
            _logger = logger;
        }

        // Lấy danh sách ngôn ngữ
        [HttpGet("GetAllNgonNgus")]
        public async Task<IActionResult> GetAllNgonNgus(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllNgonNgusAsync");
            var result = await _ngonNguService.GetAllNgonNgusAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllNgonNgusAsync với trạng thái {Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết ngôn ngữ theo Id
        [HttpGet("Detail/{ngonNguId}")]
        public async Task<IActionResult> GetDetail(int ngonNguId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail NgonNgu Id={NgonNguId}", ngonNguId);
            var result = await _ngonNguService.GetDetailAsync(ngonNguId, ct);
            return Ok(result);
        }

        // Tạo ngôn ngữ mới
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] NgonNguCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateNgonNgu với tên {TenNN}", dto.TenNN);
            var result = await _ngonNguService.CreateNgonNguAsync(dto, ct);
            _logger.LogInformation("API Response: CreateNgonNgu Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật ngôn ngữ
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] NgonNguUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateNgonNgu Id={NgonNguId}", dto.NgonNguId);
            var result = await _ngonNguService.UpdateNgonNguAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateNgonNgu Id={NgonNguId} Status={Status}", dto.NgonNguId, result.Status);
            return Ok(result);
        }

        // Xóa ngôn ngữ
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] NgonNguDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteNgonNgu Id={NgonNguId}", deleteDto.NgonNguId);
            var result = await _ngonNguService.DeleteNgonNguAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteNgonNgu Id={NgonNguId} Status={Status}", deleteDto.NgonNguId, result.Status);
            return Ok(result);
        }
    }
}
