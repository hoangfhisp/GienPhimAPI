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

        public SyncPhimJob(IPhimService phimService, ILogger<SyncPhimJob> logger, IUnitOfWork unitOfWork, IExternalApiClient externalApiClient)
        {
            _phimService = phimService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _externalApiClient = externalApiClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("🔁 Bắt đầu đồng bộ phim mới cập nhật...");

            var list = await _externalApiClient.GetLatestFilmsV3Async(page: 1);

            if (list?.Items == null || !list.Items.Any())
            {
                _logger.LogInformation("⏹ Không có phim nào để đồng bộ.");
                return;
            }

            foreach (var item in list.Items)
            {
                try
                {
                    var existing = await _unitOfWork.Phims.GetBySlugAsync(item.Slug);

                    if (existing == null)
                    {
                        await _phimService.ImportPhimFromApiAsync(item.Slug);
                        _logger.LogInformation($"🟢 Đã import phim mới: {item.Name}");
                    }
                    else if (item.Modified?.Time > existing.ThoiGianCapNhat)
                    {
                        await _phimService.UpdatePhimFromApiAsync(item.Slug);
                        _logger.LogInformation($"🟡 Đã cập nhật phim: {item.Name}");
                    }
                    else
                    {
                        _logger.LogInformation($"⏩ Bỏ qua {item.Slug} (không có thay đổi)");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"❌ Lỗi khi xử lý phim {item.Slug}");
                }
            }

            _logger.LogInformation("✅ Hoàn tất đồng bộ phim mới! Tổng số phim xử lý: {Count}", list.Items.Count);
        }

    }
}