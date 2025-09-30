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

        // GET: api/DienVien/GetAllDienViens?page=1&limit=10
        [HttpGet("GetAllDienViens")]
        public async Task<IActionResult> GetAllDienViens(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllDienViens page={Page}, limit={Limit}", page, limit);
            var result = await _dienVienService.GetAllDienViensAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllDienViens Status={Status}", result.Status);
            return Ok(result);
        }

        // POST: api/DienVien/Create
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DienVienCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateDienVien {TenDV}", dto.TenDV);
            var result = await _dienVienService.CreateDienVienAsync(dto, ct);
            _logger.LogInformation("API Response: CreateDienVien Status={Status}", result.Status);
            return Ok(result);
        }

        // PUT: api/DienVien/Update
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] DienVienUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateDienVien Id={Id}", dto.DienVienId);
            var result = await _dienVienService.UpdateDienVienAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateDienVien Id={Id} Status={Status}", dto.DienVienId, result.Status);
            return Ok(result);
        }

        // DELETE: api/DienVien/Delete?DienVienId=123
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DienVienDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteDienVien Id={Id}", deleteDto.DienVienId);
            var result = await _dienVienService.DeleteDienVienAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteDienVien Id={Id} Status={Status}", deleteDto.DienVienId, result.Status);
            return Ok(result);
        }
    }
}
