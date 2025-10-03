using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmViewController : ControllerBase
    {
        private readonly IFilmViewService _service;
        private readonly ILogger<FilmViewController> _logger;

        public FilmViewController(IFilmViewService service, ILogger<FilmViewController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // Tạo lượt xem mới (click vào phim)
        [HttpPost("Create")]
        [AllowAnonymous] // Cho phép cả khách vãng lai
        public async Task<IActionResult> Create([FromBody] FilmViewCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create FilmView cho PhimId={PhimId}, UserId={UserId}", dto.PhimId, dto.UserId);

            var result = await _service.CreateAsync(dto, ct);

            _logger.LogInformation("API Response: Create FilmView Status={Status}", result.Status);

            return Ok(result);
        }

        // Update lượt xem (vd: tăng từ 1 -> 2 khi cùng user sau thời lượng phim)
        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] FilmViewUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Update FilmView Id={FilmViewId}", dto.FilmViewId);

            var result = await _service.UpdateAsync(dto, ct);

            _logger.LogInformation("API Response: Update FilmView Id={FilmViewId} Status={Status}", dto.FilmViewId, result.Status);

            return Ok(result);
        }

        // Xoá lượt xem
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] FilmViewDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Delete FilmView Id={FilmViewId}", dto.FilmViewId);

            var result = await _service.DeleteAsync(dto, ct);

            _logger.LogInformation("API Response: Delete FilmView Id={FilmViewId} Status={Status}", dto.FilmViewId, result.Status);

            return Ok(result);
        }
    }
}
