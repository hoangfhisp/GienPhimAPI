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

        public async Task<ApiPhimListResponse?> GetLatestFilmsV3Async(int page, CancellationToken ct = default)
        {
            // ✅ Không hard-code domain, chỉ nối phần route sau base URL
            var endpoint = $"danh-sach/phim-moi-cap-nhat-v3?page={page}";
            _logger.LogInformation("🌐 Gọi API danh sách phim mới cập nhật: {Endpoint}", endpoint);

            var response = await _httpClient.GetAsync(endpoint, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("⚠️ Gọi API thất bại với mã trạng thái {StatusCode}", response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<ApiPhimListResponse>(json, options);
        }
    }
}
