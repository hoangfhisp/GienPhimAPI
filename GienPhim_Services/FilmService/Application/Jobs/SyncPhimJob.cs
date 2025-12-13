using Quartz;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.HttpClients;

namespace GienPhim_Services.FilmService.Application.Jobs
{
    public class SyncPhimJob : IJob
    {
        private readonly IPhimService _phimService;
        private readonly ILogger<SyncPhimJob> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExternalApiClient _externalApiClient;
        private readonly IBotConfigService _botConfigService;

        public SyncPhimJob(
            IPhimService phimService,
            ILogger<SyncPhimJob> logger,
            IUnitOfWork unitOfWork,
            IExternalApiClient externalApiClient,
            IBotConfigService botConfigService)
        {
            _phimService = phimService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _externalApiClient = externalApiClient;
            _botConfigService = botConfigService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Bắt đầu tiến trình đồng bộ phim theo cấu hình bot...");

            // Lấy cấu hình bot hiện tại
            var configResult = await _botConfigService.GetBotConfigAsync();
            var config = configResult.Data;

            if (config == null)
            {
                _logger.LogWarning("Không tìm thấy cấu hình bot trong DB. Dừng tiến trình.");
                return;
            }

            // Kiểm tra trạng thái bot
            if (!config.IsEnabled)
            {
                _logger.LogInformation("Bot đang bị tắt (IsEnabled = false). Bỏ qua lần đồng bộ này.");
                return;
            }

            _logger.LogInformation("BotConfig hiện tại: ApiPerRun = {ApiPerRun}, IntervalHours = {IntervalHours}",
                config.ApiPerRun, config.IntervalHours);

            // Vòng lặp lấy API theo cấu hình
            for (int page = 1; page <= config.ApiPerRun; page++)
            {
                _logger.LogInformation("Đang lấy dữ liệu từ trang API {Page}...", page);

                var list = await _externalApiClient.GetLatestFilmsV3Async(page);

                if (list?.Items == null || !list.Items.Any())
                {
                    _logger.LogInformation("Trang {Page} không có dữ liệu phim mới. Dừng tại đây.", page);
                    break;
                }

                foreach (var item in list.Items)
                {
                    try
                    {
                        var existing = await _unitOfWork.Phims.GetBySlugAsync(item.Slug);

                        if (existing == null)
                        {
                            await _phimService.ImportPhimFromApiAsync(item.Slug);
                            _logger.LogInformation($"Đã import phim mới: {item.Name}");
                        }
                        else if (item.Modified?.Time > existing.ThoiGianCapNhat)
                        {
                            await _phimService.UpdatePhimFromApiAsync(item.Slug);
                            _logger.LogInformation($"Đã cập nhật phim: {item.Name}");
                        }
                        else
                        {
                            _logger.LogInformation($"Bỏ qua {item.Slug} (không có thay đổi)");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Lỗi khi xử lý phim {item.Slug}");
                    }
                }
            }

            _logger.LogInformation("Hoàn tất đồng bộ phim mới theo cấu hình bot!");
        }
    }
}
