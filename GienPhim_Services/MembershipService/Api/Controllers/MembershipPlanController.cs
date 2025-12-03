using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.MembershipService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipPlanController : ControllerBase
    {
        private readonly IMembershipPlanService _membershipPlanService;
        private readonly ILogger<MembershipPlanController> _logger;

        public MembershipPlanController(
            ILogger<MembershipPlanController> logger,
            IMembershipPlanService membershipPlanService)
        {
            _logger = logger;
            _membershipPlanService = membershipPlanService;
        }

        // Lấy danh sách MembershipPlan (GET)
        [HttpGet("GetAllMembershipPlans")]
        public async Task<IActionResult> GetAllMembershipPlans(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetAllMembershipPlansAsync");
            var result = await _membershipPlanService.GetAllMembershipPlansAsync(page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetAllMembershipPlansAsync với trạng thái {Status}", result.Status);
            return Ok(result);
        }

        // Lấy chi tiết MembershipPlan theo Id
        [HttpGet("Detail/{planId}")]
        public async Task<IActionResult> GetDetail(string planId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetDetail MembershipPlan Id={PlanId}", planId);
            var result = await _membershipPlanService.GetDetailAsync(planId, ct);
            _logger.LogInformation("API Response: GetDetail MembershipPlan Id={PlanId} Status={Status}", planId, result.Status);
            return Ok(result);
        }

        // Tạo mới MembershipPlan
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MembershipPlanCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateMembershipPlan với tên {TenPlan}", dto.TenPlan);
            var result = await _membershipPlanService.CreateMembershipPlanAsync(dto, ct);
            _logger.LogInformation("API Response: CreateMembershipPlan Status={Status}", result.Status);
            return Ok(result);
        }

        // Cập nhật MembershipPlan
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] MembershipPlanUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: UpdateMembershipPlan Id={PlanId}", dto.PlanId);
            var result = await _membershipPlanService.UpdateMembershipPlanAsync(dto, ct);
            _logger.LogInformation("API Response: UpdateMembershipPlan Id={PlanId} Status={Status}", dto.PlanId, result.Status);
            return Ok(result);
        }

        // Xoá MembershipPlan
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] MembershipPlanDeleteDTO deleteDto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: DeleteMembershipPlan Id={PlanId}", deleteDto.PlanId);
            var result = await _membershipPlanService.DeleteMembershipPlanAsync(deleteDto, ct);
            _logger.LogInformation("API Response: DeleteMembershipPlan Id={PlanId} Status={Status}", deleteDto.PlanId, result.Status);
            return Ok(result);
        }
    }
}
