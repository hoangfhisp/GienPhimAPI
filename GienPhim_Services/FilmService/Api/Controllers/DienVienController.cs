using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DienVienController : ControllerBase
    {
        private readonly IDienVienService _dienVienService;
        private readonly ILogger<DienVienController> _logger;

        public DienVienController(IDienVienService dienVienService, ILogger<DienVienController> logger)
        {
            _dienVienService = dienVienService;
            _logger = logger;
        }

        // Lấy danh sách diễn viên
        [HttpGet("GetAllDienViens")]
        public async Task<IActionResult> GetAllDienViens(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllDienViens page={Page}, limit={Limit}", page, limit);
            var result = await _dienVienService.GetAllDienViensAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllDienViens Status={Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết diễn viên theo ID
        [HttpGet("Detail/{dienVienId}")]
        public async Task<IActionResult> GetDetail(int dienVienId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail DienVien Id={DienVienId}", dienVienId);

            var result = await _dienVienService.GetDetailAsync(dienVienId, ct);

            _logger.LogInformation("API Response: GetDetail DienVien Id={DienVienId} Status={Status}", dienVienId, result.Status);

            return Ok(result);
        }

        // Thêm mới diễn viên
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DienVienCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateDienVien {TenDV}", dto.TenDV);
            var result = await _dienVienService.CreateDienVienAsync(dto, ct);
            _logger.LogInformation("API Response: CreateDienVien Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật thông tin diễn viên
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] DienVienUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateDienVien Id={Id}", dto.DienVienId);
            var result = await _dienVienService.UpdateDienVienAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateDienVien Id={Id} Status={Status}", dto.DienVienId, result.Status);
            return Ok(result);
        }

        // Xóa diễn viên
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DienVienDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteDienVien Id={Id}", deleteDto.DienVienId);
            var result = await _dienVienService.DeleteDienVienAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteDienVien Id={Id} Status={Status}", deleteDto.DienVienId, result.Status);
            return Ok(result);
        }

        // Tìm kiếm diễn viên theo từ khóa
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: SearchDienViens với từ khóa {Keyword}", keyword);

            var result = await _dienVienService.SearchDienViensAsync(keyword, ct);

            _logger.LogInformation("API Response: SearchDienViens Status={Status}, Tổng={Count}", result.Status, result.Data.Count);

            return Ok(result);
        }
    }
}
