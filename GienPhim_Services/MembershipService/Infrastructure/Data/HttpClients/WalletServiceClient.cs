using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Http;
using GienPhim_Services.MembershipService.Application.DTOs.Wallets;
using GienPhim_Services.MembershipService.Infrastructure.HttpClients.Interfaces;

namespace GienPhim_Services.MembershipService.Infrastructure.HttpClients
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

        // lấy thong tin ví theo userId
        public async Task<ApiResponse<ResultDTO<WalletHttpResponseDTO>>> GetWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("WalletServiceClient: Gọi API GetWalletByUserId với userId: {UserId}", userId);
            var response = await _httpClient.GetAsync($"api/Wallet/GetWalletByUserId?userId={userId}", cancellationToken);
            var body = await response.Content.ReadFromJsonAsync<ResultDTO<WalletHttpResponseDTO>>(
                cancellationToken: cancellationToken);
            _logger.LogInformation("WalletServiceClient: Kết quả từ API GetWalletByUserId với userId: {UserId} là {Response}", userId, body);
            return new ApiResponse<ResultDTO<WalletHttpResponseDTO>>
            {
                Data = body!,
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<ApiResponse<ResultDTO>> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("WalletServiceClient: Gọi API GetWalletByUserId với userId: {UserId}", request.WalletId);
            var response = await _httpClient.PostAsJsonAsync($"api/Wallet/CreateWalletTransaction", request, cancellationToken);
            var body = await response.Content.ReadFromJsonAsync<ResultDTO>(
                cancellationToken: cancellationToken);
            _logger.LogInformation("WalletServiceClient: Kết quả từ API GetWalletByUserId với userId: {UserId} là {Response}", request.WalletId, body);
            return new ApiResponse<ResultDTO>
            {
                Data = body!,
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
