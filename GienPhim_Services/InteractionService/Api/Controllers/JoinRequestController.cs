using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.InteractionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinRequestController : ControllerBase
    {
        private readonly IJoinRequestService _joinRequestService;
        private readonly ILogger<JoinRequestController> _logger;

        public JoinRequestController(
            ILogger<JoinRequestController> logger,
            IJoinRequestService joinRequestService)
        {
            _logger = logger;
            _joinRequestService = joinRequestService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] JoinRequestCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: CreateJoinRequest GroupId={GroupId}", dto.GroupId);

            var result = await _joinRequestService.CreateJoinRequestAsync(dto, ct);

            _logger.LogInformation("API Response: CreateJoinRequest GroupId={GroupId} Status={Status}", dto.GroupId, result.Status);

            return Ok(result);
        }

        [HttpGet("ByGroup/{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByGroup(Guid groupId, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: GetJoinRequestsByGroup GroupId={GroupId}, Status={Status}, Page={Page}, PageSize={PageSize}", groupId, status, page, pageSize);

            var result = await _joinRequestService.GetByGroupAsync(groupId, status, page, pageSize, ct);

            _logger.LogInformation("API Response: GetJoinRequestsByGroup GroupId={GroupId} Status={Status}", groupId, result.Status);

            return Ok(result);
        }

        [HttpPost("Accept/{requestId:guid}")]
        [Authorize]
        public async Task<IActionResult> Accept(Guid requestId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: AcceptJoinRequest RequestId={RequestId}", requestId);

            var result = await _joinRequestService.AcceptAsync(requestId, ct);

            _logger.LogInformation("API Response: AcceptJoinRequest RequestId={RequestId} Status={Status}", requestId, result.Status);

            return Ok(result);
        }

        [HttpPost("Reject/{requestId:guid}")]
        [Authorize]
        public async Task<IActionResult> Reject(Guid requestId, CancellationToken ct = default)
        {
            _logger.LogInformation("API Request: RejectJoinRequest RequestId={RequestId}", requestId);

            var result = await _joinRequestService.RejectAsync(requestId, ct);

            _logger.LogInformation("API Response: RejectJoinRequest RequestId={RequestId} Status={Status}", requestId, result.Status);

            return Ok(result);
        }
    }
}
