using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuocGiaController : ControllerBase
    {
        private readonly IQuocGiaService _quocGiaService;
        private readonly ILogger<QuocGiaController> _logger;

        public QuocGiaController(IQuocGiaService quocGiaService, ILogger<QuocGiaController> logger)
        {
            _quocGiaService = quocGiaService;
            _logger = logger;
        }

        // Lấy danh sách quốc gia
        [HttpGet("GetAllQuocGias")]
        public async Task<IActionResult> GetAllQuocGias(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllQuocGiasAsync Page={Page}, Limit={Limit}", page, limit);
            var result = await _quocGiaService.GetAllQuocGiasAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllQuocGiasAsync Status={Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết quốc gia theo ID
        [HttpGet("Detail/{quocGiaId}")]
        public async Task<IActionResult> GetDetail(int quocGiaId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail QuocGia Id={QuocGiaId}", quocGiaId);

            var result = await _quocGiaService.GetDetailAsync(quocGiaId, ct);

            _logger.LogInformation("API Response: GetDetail QuocGia Id={QuocGiaId} Status={Status}", quocGiaId, result.Status);

            return Ok(result);
        }

        // Tạo mới quốc gia
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] QuocGiaCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateQuocGia với tên {TenQG}", dto.TenQG);
            var result = await _quocGiaService.CreateQuocGiaAsync(dto, ct);
            _logger.LogInformation("API Response: CreateQuocGia Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật quốc gia
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] QuocGiaUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateQuocGia Id={QuocGiaId}", dto.QuocGiaId);
            var result = await _quocGiaService.UpdateQuocGiaAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateQuocGia Id={QuocGiaId} Status={Status}", dto.QuocGiaId, result.Status);
            return Ok(result);
        }

        // Xóa quốc gia
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] QuocGiaDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteQuocGia Id={QuocGiaId}", deleteDto.QuocGiaId);
            var result = await _quocGiaService.DeleteQuocGiaAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteQuocGia Id={QuocGiaId} Status={Status}", deleteDto.QuocGiaId, result.Status);
            return Ok(result);
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: SearchQuocGias với từ khóa {Keyword}", keyword);

            var result = await _quocGiaService.SearchQuocGiasAsync(keyword, ct);

            _logger.LogInformation("API Response: SearchQuocGias Status={Status}, Tổng={Count}", result.Status, result.Data.Count);

            return Ok(result);
        }
    }
}
