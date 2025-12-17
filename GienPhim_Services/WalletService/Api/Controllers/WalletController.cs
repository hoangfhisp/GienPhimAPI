using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GienPhim_Services.WalletService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletController> _logger;

        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [HttpGet("GetWalletByUserId")]
        [Authorize]
        public async Task<IActionResult> GetWalletByUserId(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu GetWalletByUserId với userId: {userId}", userId);
            var wallet = await _walletService.GetWalletByUserIdAsync(userId, cancellationToken);
            _logger.LogInformation("API Response: GetWalletByUserId với trạng thái {status}", wallet.Status);
            return Ok(wallet);
        }

        // Tạo ví mới
        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletRequestDTO request,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu CreateWallet ");
            var wallet = await _walletService.CreateWalletAsync(request,cancellationToken);
            _logger.LogInformation("API Response: CreateWallet với trạng thái {status}",wallet.Status);
            return Ok(wallet);
        }

        [HttpGet("GetWalletTransactionByWalletId")]
        [Authorize]
        public async Task<IActionResult> GetWalletTransactionByWalletId(string walletId, int page = 1, int limit = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu GetWalletTransactionByWalletId với walletId: {walletId}, page: {page}, limit: {limit}", walletId, page, limit);
            var transactions = await _walletService.GetWalletTransactionByWalletId(walletId, page, limit, cancellationToken);
            _logger.LogInformation("API Response: GetWalletTransactionByWalletId với trạng thái {status}", transactions.Status);
            return Ok(transactions);
        }

        [HttpPost("CreateWalletTransaction")]
        [Authorize]
        public async Task<IActionResult> CreateWalletTransaction(CreateWalletTransactionRequestDTO request,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu CreateWalletTransaction ");
            var result = await _walletService.CreateWalletTransactionAsync(request, cancellationToken);
            _logger.LogInformation("API Response: CreateWalletTransaction với trạng thái {status}", result.Status);
            return Ok(result);
        }
    }
}
