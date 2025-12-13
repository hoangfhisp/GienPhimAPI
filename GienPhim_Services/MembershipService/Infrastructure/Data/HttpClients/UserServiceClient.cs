using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Client.User;
using GienPhim_Services.MembershipService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.MembershipService.Infrastructure.Data.HttpClients
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

        public async Task<ApiResponse<ResultDTO<UserHttpResponseDTO>>?> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("UserServiceClient: Gọi API UserService để lấy thông tin user với UserId: {UserId}", userId);
            var response = await _httpClient.GetAsync($"api/User/GetUserByUserId?userId={userId}", cancellationToken);
            // Đọc body JSON
            var body = await response.Content.ReadFromJsonAsync<ResultDTO<UserHttpResponseDTO>>(cancellationToken: cancellationToken);
            _logger.LogInformation("UserServiceClient: API UserService trả về mã trạng thái: {StatusCode}", (int)response.StatusCode);
            // Trả về wrapper có cả status code
            return new ApiResponse<ResultDTO<UserHttpResponseDTO>>
            {
                Data = body,
                StatusCode = (int)response.StatusCode
            };
        }

    }
}
