using GienPhim_Services.WalletService.Application.DTOs.Payments;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace GienPhim_Services.WalletService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WalletController> _logger;

        public WalletController(IWalletService walletService,IConfiguration configuration
            , ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("GetWalletByUserId")]
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
        public async Task<IActionResult> CreateWalletTransaction(CreateWalletTransactionRequestDTO request,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu CreateWalletTransaction ");
            var result = await _walletService.CreateWalletTransactionAsync(request, cancellationToken);
            _logger.LogInformation("API Response: CreateWalletTransaction với trạng thái {status}", result.Status);
            return Ok(result);
        }

        [HttpPost("CreateVnPay")]
        public async Task<IActionResult> Create([FromBody] PaymentRequestDTO model, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: Create VnPay Payment for SoTien={SoTien}", model.SoTien);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _walletService.CreateVnPayPayment(model, ip,cancellationToken);
            _logger.LogInformation("API Response: Create VnPay Payment for SoTien={SoTien} Status={Status}", model.SoTien, result.Status);
            return Ok(result);
        }

        [HttpGet("CallbackVnPay")]
        public async Task<IActionResult> Callback(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Request: VnPay Callback with Query Parameters");

            var result = await _walletService.HandleVnPayCallback(Request.Query, cancellationToken);

            var feUrl = _configuration["Frontend:PaymentResultUrl"];

            if (result.Status) // thanh toán thành công
            {
                return Redirect(
                    $"{feUrl}" +
                    $"?status=success" +
                    $"&txnRef={result.Data.OrderId}" +
                    $"&code={ result.Data.ResponseCode}"+
                    $"&amount={result.Data.Amount}"
                );
            }

            // thất bại
            return Redirect(
                $"{feUrl}" +
                $"?status=fail" +
                $"&txnRef={result.Data?.OrderId}" +
                $"&code={result.Data.ResponseCode}" +
                $"&amount={result.Data?.Amount}"
            );
        }
    }
}
