using Quartz;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.HttpClients;
using GienPhim_Services.FilmService.Application.DTOs.Client.BotSyncLogs;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Jobs
{
    public class SyncPhimJob : IJob
    {
        private readonly IPhimService _phimService;
        private readonly ILogger<SyncPhimJob> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExternalApiClient _externalApiClient;
        private readonly IBotConfigService _botConfigService;
        private readonly IBotSyncLogService _botSyncLogService;

        public SyncPhimJob(
            IPhimService phimService,
            ILogger<SyncPhimJob> logger,
            IUnitOfWork unitOfWork,
            IExternalApiClient externalApiClient,
            IBotConfigService botConfigService,
            IBotSyncLogService botSyncLogService)
        {
            _phimService = phimService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _externalApiClient = externalApiClient;
            _botConfigService = botConfigService;
            _botSyncLogService = botSyncLogService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Bắt đầu tiến trình đồng bộ phim theo cấu hình bot...");

            // Lấy cấu hình bot hiện tại
            var configResult = await _botConfigService.GetBotConfigAsync(context.CancellationToken);
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

                            await _botSyncLogService.CreateAsync(new BotSyncLogCreateDTO
                            {
                                PhimName = item.Name,
                                PhimSlug = item.Slug,
                                Action = BotSyncAction.Import,
                                Message = "Import phim mới từ API"
                            }, context.CancellationToken);

                            _logger.LogInformation("Đã import phim mới: {Name}", item.Name);
                        }
                        else if (item.Modified?.Time > existing.ThoiGianCapNhat)
                        {
                            await _phimService.UpdatePhimFromApiAsync(item.Slug);

                            await _botSyncLogService.CreateAsync(new BotSyncLogCreateDTO
                            {
                                PhimName = item.Name,
                                PhimSlug = item.Slug,
                                Action = BotSyncAction.Update,
                                Message = "Cập nhật phim do có thay đổi từ API"
                            }, context.CancellationToken);

                            _logger.LogInformation("Đã cập nhật phim: {Name}", item.Name);
                        }
                        else
                        {
                            await _botSyncLogService.CreateAsync(new BotSyncLogCreateDTO
                            {
                                PhimName = item.Name,
                                PhimSlug = item.Slug,
                                Action = BotSyncAction.Skip,
                                Message = "Không có thay đổi"
                            }, context.CancellationToken);

                            _logger.LogInformation("Bỏ qua {Slug} (không có thay đổi)", item.Slug);
                        }
                    }
                    catch (Exception ex)
                    {
                        // cố gắng ghi log lỗi vào DB (đừng để lỗi log làm crash job)
                        try
                        {
                            await _botSyncLogService.CreateAsync(new BotSyncLogCreateDTO
                            {
                                PhimName = item.Name,
                                PhimSlug = item.Slug,
                                Action = BotSyncAction.Error,
                                Message = ex.Message
                            }, context.CancellationToken);
                        }
                        catch (Exception logEx)
                        {
                            _logger.LogError(logEx, "Không thể ghi BotSyncLog cho phim {Slug}", item.Slug);
                        }

                        _logger.LogError(ex, "Lỗi khi xử lý phim {Slug}", item.Slug);
                    }
                }
            }

            _logger.LogInformation("Hoàn tất đồng bộ phim mới theo cấu hình bot!");
        }
    }
}
