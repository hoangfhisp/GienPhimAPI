using GienPhim_Services.FilmService.Application.Interfaces.Services;
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

    }
}
