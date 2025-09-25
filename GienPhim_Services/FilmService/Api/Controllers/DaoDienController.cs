using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.FilmService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaoDienController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IDaoDienService _daoDienService;
        private readonly ILogger<DaoDienController> _logger;
        public DaoDienController(IFilmService filmService, ILogger<DaoDienController> logger)
        {
            _filmService = filmService;
            _logger = logger;
        }

        // FILM SERVICE
        // Lấy danh sách đạo diễn
        [HttpGet("GetAllDaoDiens")]
        public async Task<IActionResult> GetAllDaoDiens(int page = 1,int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllDaoDiensAsync ");
            var result = await _filmService.GetAllDaoDiensAsync(page,limit,cancellationToken);
            _logger.LogInformation("API Response: GetAllDaoDiensAsync với trạng thái {Status}", result.Status);
            return Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DaoDienCreateDTO dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ResultDTO.Fail("Dữ liệu không hợp lệ"));

            var result = await _daoDienService.CreateDaoDienAsync(dto, ct);
            if (!result.Status) return BadRequest(result);

            // Nếu service trả về Id trong Data thì có thể dùng CreatedAtAction
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DaoDienUpdateDTO dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ResultDTO.Fail("Dữ liệu không hợp lệ"));

            var result = await _daoDienService.UpdateDaoDienAsync(dto, ct);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct = default)
        {
            var result = await _daoDienService.DeleteDaoDienAsync(new DaoDienDeleteDTO { DaoDienId = id }, ct);
            // Nếu không tìm thấy → service trả Fail với message "Không tìm thấy..." → 404 hợp lý
            return result.Status ? Ok(result) : NotFound(result);
        }
    }
}
