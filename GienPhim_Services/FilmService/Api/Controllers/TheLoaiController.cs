using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiController : ControllerBase
    {
        private readonly ITheLoaiService _theLoaiService;
        private readonly ILogger<TheLoaiController> _logger;

        public TheLoaiController( ILogger<TheLoaiController> logger, ITheLoaiService theLoaiService)
        {
            _theLoaiService = theLoaiService;
            _logger = logger;
        }

        // Lấy danh sách thể loại
        [HttpGet("GetAllTheLoais")]
        public async Task<IActionResult> GetAllTheLoais(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllTheLoaisAsync");
            var result = await _theLoaiService.GetAllTheLoaisAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllTheLoaisAsync Status={Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết thể loại theo ID
        [HttpGet("Detail/{theLoaiId}")]
        public async Task<IActionResult> GetDetail(int theLoaiId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail TheLoai Id={TheLoaiId}", theLoaiId);

            var result = await _theLoaiService.GetDetailAsync(theLoaiId, ct);

            _logger.LogInformation("API Response: GetDetail TheLoai Id={TheLoaiId} Status={Status}", theLoaiId, result.Status);

            return Ok(result);
        }

        // Tạo mới thể loại
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TheLoaiCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateTheLoai với tên {TenTL}", dto.TenTL);
            var result = await _theLoaiService.CreateTheLoaiAsync(dto, ct);
            _logger.LogInformation("API Response: CreateTheLoai Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật thể loại
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] TheLoaiUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateTheLoai Id={TheLoaiId}", dto.TheLoaiId);
            var result = await _theLoaiService.UpdateTheLoaiAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateTheLoai Id={TheLoaiId} Status={Status}", dto.TheLoaiId, result.Status);
            return Ok(result);
        }

        // Xóa thể loại
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] TheLoaiDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteTheLoai Id={TheLoaiId}", deleteDto.TheLoaiId);
            var result = await _theLoaiService.DeleteTheLoaiAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteTheLoai Id={TheLoaiId} Status={Status}", deleteDto.TheLoaiId, result.Status);
            return Ok(result);
        }

        // Tìm kiếm thể loại theo từ khóa
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: SearchTheLoais với từ khóa {Keyword}", keyword);

            var result = await _theLoaiService.SearchTheLoaisAsync(keyword, ct);

            _logger.LogInformation("API Response: SearchTheLoais Status={Status}, Tổng={Count}", result.Status, result.Data.Count);

            return Ok(result);
        }
    }
}
