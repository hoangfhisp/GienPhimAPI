using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GienPhim_Services.FilmService.Application.Services;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IPhimService _phimService;
        private readonly ILogger<PhimController> _logger;

        public PhimController(IPhimService phimService, ILogger<PhimController> logger)
        {
            _phimService = phimService;
            _logger = logger;
        }

        // Lấy danh sách phim (có phân trang)
        [HttpGet("GetAllPhims")]
        public async Task<IActionResult> GetAllPhims(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllPhimsAsync page={Page}, limit={Limit}", page, limit);

            var result = await _phimService.GetAllPhimsAsync(page, limit, cancellationToken);

            _logger.LogInformation("API Response: GetAllPhimsAsync Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }

        // Xem chi tiết phim
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetById Phim Id={PhimId}", id);

            var result = await _phimService.GetByIdAsync(id, ct);

            _logger.LogInformation("API Response: GetById Phim Id={PhimId} Status={Status}", id, result.Status);

            return Ok(result);
        }

        // Tạo phim mới
        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] PhimCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreatePhim với tên {TenP}", dto.TenP);

            var result = await _phimService.CreatePhimAsync(dto, ct);

            _logger.LogInformation("API Response: CreatePhim Status={Status}", result.Status);

            return Ok(result);
        }

        // Cập nhật phim
        [HttpPut("Update")]
        [Consumes("multipart/form-data")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] PhimUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdatePhim Id={PhimId}", dto.PhimId);

            var result = await _phimService.UpdatePhimAsync(dto, ct);

            _logger.LogInformation("API Response: UpdatePhim Id={PhimId} Status={Status}", dto.PhimId, result.Status);

            return Ok(result);
        }

        // Xóa phim
        [HttpDelete("Delete")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] PhimDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeletePhim Id={PhimId}", dto.PhimId);

            var result = await _phimService.DeletePhimAsync(dto, ct);

            _logger.LogInformation("API Response: DeletePhim Id={PhimId} Status={Status}", dto.PhimId, result.Status);

            return Ok(result);
        }

        // Nhập phim từ API ngoài
        [HttpPost("ImportFromApi")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportFromApi([FromQuery] string slug, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: ImportFromApi slug={Slug}", slug);

            var result = await _phimService.ImportPhimFromApiAsync(slug, cancellationToken);

            _logger.LogInformation("API Response: ImportFromApi slug={Slug} Status={Status}", slug, result.Status);

            return Ok(result);
        }

        // Cập nhật phim từ API ngoài
        [HttpPut("UpdateFromApi")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFromApi([FromQuery] string slug, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: UpdateFromApi slug={Slug}", slug);

            var result = await _phimService.UpdatePhimFromApiAsync(slug, cancellationToken);

            _logger.LogInformation("API Response: UpdateFromApi slug={Slug} Status={Status}", slug, result.Status);

            return Ok(result);
        }

        // Tìm kiếm phim theo từ khóa
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: SearchPhims với từ khóa {Keyword}", keyword);

            var result = await _phimService.SearchPhimsAsync(keyword, ct);

            _logger.LogInformation("API Response: SearchPhims Status={Status}, Total={Count}", result.Status, result.Data?.Count);

            return Ok(result);
        }

        // Lọc phim theo nhiều tiêu chí
        [HttpPost("FilterPhims")]
        public async Task<IActionResult> FilterPhims([FromBody] FilterPhimDTO dto, int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: FilterPhims với filter {@Dto}", dto);

            var result = await _phimService.FilterPhimsAsync(dto, page, limit, cancellationToken);

            _logger.LogInformation("API Response: FilterPhims Status={Status}, Total={Total}", result.Status, result.pagination?.Total);

            return Ok(result);
        }
    }
}
