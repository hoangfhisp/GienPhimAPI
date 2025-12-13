using GienPhim_Services.UserService.Application.DTOs.Common;
using GienPhim_Services.UserService.Application.DTOs.Http;
using GienPhim_Services.UserService.Infrastructure.HttpClients.Interfaces;

namespace GienPhim_Services.UserService.Infrastructure.HttpClients
{
    public class WalletServiceClient : IWalletServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WalletServiceClient> _logger;
        public WalletServiceClient(HttpClient httpClient, ILogger<WalletServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Tạo ví cho user mới
        public async Task<ApiResponse<ResultDTO<WalletHttpResponseDTO>>> CreateWalletForUserAsync(CreateWalletHttpRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("WalletServiceClient: Gọi API CreateWallet user với userId: {UserId}", request.UserId);
            var response = await _httpClient.PostAsJsonAsync($"api/Wallet/CreateWallet",request, cancellationToken);

            var body = await response.Content.ReadFromJsonAsync<ResultDTO<WalletHttpResponseDTO>>(
                cancellationToken: cancellationToken);
            _logger.LogInformation("WalletServiceClient: Kết quả từ API CreateWallet user với userId: {UserId} là {Response}", request.UserId, body);
            return new ApiResponse<ResultDTO<WalletHttpResponseDTO>>
            {
                Data = body!,
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
