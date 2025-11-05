using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace GienPhim_Services.FilmService.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class BotConfigWatcherJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<BotConfigWatcherJob> _logger;
        private static DateTime _lastChecked = DateTime.MinValue;

        public BotConfigWatcherJob(
            IUnitOfWork unitOfWork,
            ISchedulerFactory schedulerFactory,
            ILogger<BotConfigWatcherJob> logger)
        {
            _unitOfWork = unitOfWork;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("🔍 Watcher: Kiểm tra thay đổi BotConfig...");

            var config = await _unitOfWork.BotConfigs.GetAsync();
            if (config == null)
            {
                _logger.LogWarning("Không tìm thấy BotConfig trong DB.");
                return;
            }

            // Nếu có thay đổi mới kể từ lần kiểm tra cuối cùng
            if (config.LastUpdated > _lastChecked)
            {
                var scheduler = await _schedulerFactory.GetScheduler();
                var jobKey = new JobKey("SyncPhimJob");
                var triggerKey = new TriggerKey("SyncPhimJob-trigger");

                // 🔎 Lấy trigger hiện tại (nếu có)
                var currentTriggers = await scheduler.GetTriggersOfJob(jobKey);
                var oldTrigger = currentTriggers.FirstOrDefault();

                // 🧩 Nếu trigger hiện tại vẫn còn thời gian hợp lệ => không reschedule ngay
                if (oldTrigger != null && oldTrigger.GetNextFireTimeUtc() > DateTimeOffset.UtcNow)
                {
                    _logger.LogInformation("⚠️ BotConfig thay đổi nhưng trigger hiện tại vẫn còn hiệu lực, bỏ qua reschedule lần này.");
                    _lastChecked = config.LastUpdated;
                    return;
                }

                _logger.LogInformation("🔁 Phát hiện BotConfig thay đổi và trigger hết hạn — tiến hành reschedule SyncPhimJob...");

                // Xóa trigger cũ nếu có
                await scheduler.UnscheduleJob(triggerKey);

                // Nếu job chưa có, tạo lại
                var jobDetail = await scheduler.GetJobDetail(jobKey)
                                 ?? JobBuilder.Create<SyncPhimJob>()
                                     .WithIdentity(jobKey)
                                     .StoreDurably()
                                     .Build();

                // Tạo trigger mới với delay theo cấu hình
                var trigger = TriggerBuilder.Create()
                    .ForJob(jobDetail)
                    .WithIdentity(triggerKey)
                    .StartAt(DateTimeOffset.Now.AddHours(config.IntervalHours)) // ⏳ chờ đúng số giờ cấu hình
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(config.IntervalHours)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(trigger);

                _lastChecked = config.LastUpdated;
                _logger.LogInformation("✅ Reload thành công: BotConfig Interval={IntervalHours}h, ApiPerRun={ApiPerRun}", config.IntervalHours, config.ApiPerRun);
            }
            else
            {
                _logger.LogInformation("Không có thay đổi trong BotConfig kể từ lần trước.");
            }
        }
    }
}
