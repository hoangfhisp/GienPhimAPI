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

        // Tạo ví mới
        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletRequestDTO request,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("API Response: Bắt đầu CreateWallet ");
            var wallet = await _walletService.CreateWalletAsync(request,cancellationToken);
            _logger.LogInformation("API Response: CreateWallet với trạng thái {status}",wallet.Status);
            return Ok(wallet);
        }
    }
}
