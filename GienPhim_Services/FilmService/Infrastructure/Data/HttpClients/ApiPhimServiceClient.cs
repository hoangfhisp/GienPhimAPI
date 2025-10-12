using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces;
using System.Text.Json;

namespace GienPhim_Services.FilmService.Infrastructure.Data.HttpClients
{
    public class ApiPhimServiceClient : IApiPhimServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiPhimServiceClient> _logger;

        public ApiPhimServiceClient(HttpClient httpClient, ILogger<ApiPhimServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiPhimDTO> GetPhimDataAsync(string slug, CancellationToken ct = default)
        {
            _logger.LogInformation("Infra: Đang gọi API phim với slug {Slug}", slug);

            // Chỉ gọi phần slug, vì BaseAddress đã có sẵn domain
            var response = await _httpClient.GetAsync(slug, ct);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(ct);
            return JsonSerializer.Deserialize<ApiPhimDTO>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Không thể deserialize dữ liệu phim.");
        }
    }
}
