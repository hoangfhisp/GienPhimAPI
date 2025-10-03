using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GienPhim_Services.FilmService.Application.Services;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaoDienController : ControllerBase
    {
        private readonly IDaoDienService _daoDienService;
        private readonly ILogger<DaoDienController> _logger;
        public DaoDienController( ILogger<DaoDienController> logger, IDaoDienService daoDienService)
        {
            _daoDienService = daoDienService;
            _logger = logger;
        }

        // Lấy danh sách đạo diễn
        [HttpGet("GetAllDaoDiens")]
        public async Task<IActionResult> GetAllDaoDiens(int page = 1,int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllDaoDiensAsync ");
            var result = await _daoDienService.GetAllDaoDiensAsync(page,limit,cancellationToken);
            _logger.LogInformation("API Response: GetAllDaoDiensAsync với trạng thái {Status}", result.Status);
            return Ok(result); 
        }

        // Lấy chi tiết đạo diễn theo Id
        [HttpGet("Detail/{daoDienId}")]
        public async Task<IActionResult> GetDetail(int daoDienId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail DaoDien Id={DaoDienId}", daoDienId);
            var result = await _daoDienService.GetDetailAsync(daoDienId, ct);
            return Ok(result);
        }

        // Tạo mới đạo diễn
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DaoDienCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateDaoDien với tên {TenDD}", dto.TenDD);
            var result = await _daoDienService.CreateDaoDienAsync(dto, ct);
            _logger.LogInformation("API Response: CreateDaoDien Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật thông tin đạo diễn
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] DaoDienUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateDaoDien Id={DaoDienId}", dto.DaoDienId);
            var result = await _daoDienService.UpdateDaoDienAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateDaoDien Id={DaoDienId} Status={Status}", dto.DaoDienId, result.Status);
            return Ok(result);
        }

        // Xóa đạo diễn
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery]DaoDienDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteDaoDien Id={DaoDienId}", deleteDto.DaoDienId);
            var result = await _daoDienService.DeleteDaoDienAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteDaoDien Id={DaoDienId} Status={Status}", deleteDto.DaoDienId, result.Status);
            return Ok(result);
        }
    }
}
