using System.Text.Json;
using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.HttpClients;

namespace GienPhim_Services.FilmService.Infrastructure.Data.HttpClients
{
    public class ExternalApiClient : IExternalApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiClient> _logger;

        public ExternalApiClient(HttpClient httpClient, ILogger<ExternalApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Gọi API danh sách phim mới cập nhật từ endpoint cố định hoặc động (có thể mở rộng từ BotConfig)
        public async Task<ApiPhimListResponse?> GetLatestFilmsV3Async(int page, CancellationToken ct = default)
        {
            // Endpoint mặc định
            string endpoint = $"danh-sach/phim-moi-cap-nhat-v3?page={page}";

            _logger.LogInformation("Gọi API danh sách phim mới cập nhật: {Endpoint}", endpoint);

            try
            {
                var response = await _httpClient.GetAsync(endpoint, ct);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Gọi API thất bại với mã trạng thái {StatusCode}", response.StatusCode);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync(ct);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<ApiPhimListResponse>(json, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi API phim ở endpoint {Endpoint}", endpoint);
                return null;
            }
        }
    }
}
