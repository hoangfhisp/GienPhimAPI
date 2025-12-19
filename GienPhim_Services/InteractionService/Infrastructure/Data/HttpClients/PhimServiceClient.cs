using GienPhim_Services.InteractionService.Application.DTOs.Client.Http;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Phim;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients
{
    public class PhimServiceClient : IPhimServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PhimServiceClient> _logger;
        public PhimServiceClient(HttpClient httpClient, ILogger<PhimServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Tìm User theo userId
        public async Task<ApiResponse<ResultDTO<PhimHttpResponseDTO>>> FindPhimByPhimIdAsync(int phimId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("PhimServiceClient: Gọi API PhimService để lấy thông tin phim với phimId: {PhimId}", phimId);
            var response = await _httpClient.GetAsync($"api/Phim/GetById/{phimId}", cancellationToken);
            // Đọc body JSON
            var body = await response.Content.ReadFromJsonAsync<ResultDTO<PhimHttpResponseDTO>>(
                cancellationToken: cancellationToken);
            _logger.LogInformation("PhimServiceClient: API UserService trả về mã trạng thái: {StatusCode}", (int)response.StatusCode);
            // Trả về wrapper có cả status code
            return new ApiResponse<ResultDTO<PhimHttpResponseDTO>>
            {
                Data = body!,
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<ApiResponse<ResultDTO>?> UpdatePhimLikeDisLikeAsync(UpdatePhimLikeDisLikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("PhimServiceClient: Gọi API PhimService để cập nhật lượt like/dislike cho phimId: {PhimId}", request.PhimId);
            var response = await _httpClient.PostAsJsonAsync("api/Phim/UpdatePhimByLuotLikeDisLike", request, cancellationToken);

            // Đọc body JSON
            var body = await response.Content.ReadFromJsonAsync<ResultDTO>(
                cancellationToken: cancellationToken);

            _logger.LogInformation("PhimServiceClient: API PhimService trả về mã trạng thái: {StatusCode}", (int)response.StatusCode);
            // Trả về wrapper có cả status code
            return new ApiResponse<ResultDTO>
            {
                Data = body,
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
