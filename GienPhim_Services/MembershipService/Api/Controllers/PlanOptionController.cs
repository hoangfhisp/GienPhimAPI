using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.MembershipService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanOptionController : ControllerBase
    {
        private readonly IPlanOptionService _planOptionService;
        private readonly ILogger<PlanOptionController> _logger;

        public PlanOptionController(ILogger<PlanOptionController> logger, IPlanOptionService planOptionService)
        {
            _planOptionService = planOptionService;
            _logger = logger;
        }

        // Lấy danh sách PlanOption (có phân trang)
        [HttpGet("GetAllPlanOptions")]
        public async Task<IActionResult> GetAllPlanOptions(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllPlanOptionsAsync");
            var result = await _planOptionService.GetAllPlanOptionsAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllPlanOptionsAsync với trạng thái {Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết PlanOption theo Id
        [HttpGet("Detail/{optionId}")]
        public async Task<IActionResult> GetDetail(string optionId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail PlanOption Id={OptionId}", optionId);
            var result = await _planOptionService.GetDetailAsync(optionId, ct);
            return Ok(result);
        }

        // Tạo mới PlanOption
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PlanOptionCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: Create PlanOption cho PlanId={PlanId}", dto.PlanId);
            var result = await _planOptionService.CreatePlanOptionAsync(dto, ct);
            _logger.LogInformation("API Response: Create PlanOption PlanId={PlanId} Status={Status}", dto.PlanId, result.Status);
            return Ok(result);
        }

        // Cập nhật PlanOption
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] PlanOptionUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdatePlanOption Id={OptionId}", dto.OptionId);
            var result = await _planOptionService.UpdatePlanOptionAsync(dto, ct);
            _logger.LogInformation("API Response: UpdatePlanOption Id={OptionId} Status={Status}", dto.OptionId, result.Status);
            return Ok(result);
        }

        // Xóa PlanOption
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] PlanOptionDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeletePlanOption Id={OptionId}", deleteDto.OptionId);
            var result = await _planOptionService.DeletePlanOptionAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeletePlanOption Id={OptionId} Status={Status}", deleteDto.OptionId, result.Status);
            return Ok(result);
        }
    }
}
