using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.User;
using GienPhim_Services.WalletService.Infrastructure.HttpClients.Interfaces;

namespace GienPhim_Services.WalletService.Infrastructure.HttpClients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserServiceClient> _logger;

        public UserServiceClient(HttpClient httpClient, ILogger<UserServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Tìm User theo id
        public async Task<ApiResponse<ResultDTO<UserHttpResponseDTO>>> FindUserByUserIdAsync (string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("UserServiceClient: Gọi API UserService để lấy thông tin user với userId: {UserId}", userId);
            var response = await _httpClient.GetAsync($"api/User/GetUserByUserId?userId={userId}", cancellationToken);
            // Đọc body JSON
            var body = await response.Content.ReadFromJsonAsync<ResultDTO<UserHttpResponseDTO>>(
                cancellationToken: cancellationToken);
            _logger.LogInformation("UserServiceClient: API UserService trả về mã trạng thái: {StatusCode}", (int)response.StatusCode);
            // Trả về wrapper có cả status code
            return new ApiResponse<ResultDTO<UserHttpResponseDTO>>
            {
                Data = body!,
                StatusCode = (int)response.StatusCode
            };
        }

    }
}
