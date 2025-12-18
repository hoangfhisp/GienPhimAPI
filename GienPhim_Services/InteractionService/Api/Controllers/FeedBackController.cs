using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.InteractionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;
        private readonly ILogger<FeedBackController> _logger;

        public FeedBackController(IFeedBackService feedBackService, ILogger<FeedBackController> logger)
        {
            _feedBackService = feedBackService;
            _logger = logger;
        }

        [HttpGet("GetLikeDislikeByUserIdAndPhimId")]
        [Authorize]
        public async Task<IActionResult> GetLikeDislikeByUserIdAndPhimId([FromQuery] GetFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetLikeDislikeByUserIdAndPhimId với userId: {UserId} và phimId: {PhimId}", request.UserId, request.PhimId);
            var result = await _feedBackService.GetLikeDislikeByUserIdAndPhimIdAsync(request, cancellationToken);
            _logger.LogInformation("API Response: GetLikeDislikeByUserIdAndPhimId với userId: {UserId} và phimId: {PhimId} Status={Status}", request.UserId, request.PhimId, result.Status);
            return Ok(result);
        }

        [HttpPost("ProcessingLikeDislike")]
        [Authorize]
        public async Task<IActionResult> ProcessingLikeDislike(ProcessingFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: GetLikeDislikeByUserIdAndPhimId với userId: {UserId} và phimId: {PhimId}", request.UserId, request.PhimId);
            var result = await _feedBackService.ProcessingLikeDislikeAsync(request, cancellationToken);
            _logger.LogInformation("API Response: GetLikeDislikeByUserIdAndPhimId với userId: {UserId} và phimId: {PhimId} Status={Status}", request.UserId, request.PhimId, result.Status);
            return Ok(result);
        }
    }
}
