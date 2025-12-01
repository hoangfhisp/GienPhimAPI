using GienPhim_Services.AuthService;
using GienPhim_Services.FilmService;
using GienPhim_Services.Filters;
using GienPhim_Services.MembershipService;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using GienPhim_Services.OrderService.Data;
using GienPhim_Services.PaymentService.Data;
using GienPhim_Services.UserService;
using GienPhim_Services.WalletService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quartz;
using GienPhim_Services.FilmService.Application.Jobs;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Infrastructure.Data;

System.Net.ServicePointManager.SecurityProtocol =
    System.Net.SecurityProtocolType.Tls12 |
    System.Net.SecurityProtocolType.Tls13;

var builder = WebApplication.CreateBuilder(args);
// Set UTF-8 hỗ trợ tiếng việt khi console log
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Cấu hình cho nhập token Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Thêm cấu hình JWT Auth
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập 'Bearer {token}' để xác thực"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[]{}
        }
    });

    c.CustomSchemaIds(type => type.FullName); // dùng FullName thay vì Name
});

// FILLTER
// Tắt validation mặc định của [ApiController]
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
// Đăng ký Filter toàn bộ API
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

// Configure database contexts
// InteractionService
builder.Services.AddDbContext<InteractionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InteractionDb")));
// MembershipService
builder.Services.AddDbContext<MembershipDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MembershipDb")));
// PaymentService
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentDb")));
// WalletService
builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WalletDb")));
// OrderService
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));


// ADD MICROSERVICE
builder.Services.AddUserService(builder.Configuration); // UserService
builder.Services.AddAuthService(builder.Configuration); // AuthService
builder.Services.AddFilmService(builder.Configuration); // FilmService
builder.Services.AddMembershipService(builder.Configuration); // MembershipService

// Đăng ký Quartz Scheduler
builder.Services.AddQuartz(q =>
{
    // KHÔNG cần trigger mặc định cho SyncPhimJob nữa
    q.AddJob<SyncPhimJob>(opts => opts
        .WithIdentity("SyncPhimJob")
        .StoreDurably());

    // Watcher job kiểm tra thay đổi BotConfig
    q.AddJob<BotConfigWatcherJob>(opts => opts.WithIdentity("BotConfigWatcherJob"));
    q.AddTrigger(opts => opts
    .ForJob("BotConfigWatcherJob")
    .WithIdentity("BotConfigWatcherJob-trigger")
    // Bắt đầu sau 5 phút, tránh chạy ngay khi app start
    .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute))
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(5)
        .RepeatForever()
        .WithMisfireHandlingInstructionNextWithRemainingCount()));
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

var app = builder.Build();

// Khi app khởi động: cấu hình lại Quartz dựa theo BotConfig trong DB
using (var scope = app.Services.CreateScope())
{
    var schedulerFactory = scope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
    var scheduler = await schedulerFactory.GetScheduler();

    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var botConfig = await unitOfWork.BotConfigs.GetAsync();

    if (botConfig != null && botConfig.IsEnabled)
    {
        var jobKey = new JobKey("SyncPhimJob");

        var jobDetail = await scheduler.GetJobDetail(jobKey)
                         ?? JobBuilder.Create<SyncPhimJob>()
                             .WithIdentity(jobKey)
                             .StoreDurably()
                             .Build();

        if (await scheduler.CheckExists(jobKey))
        {
            Console.WriteLine("🔁 Job SyncPhimJob đã tồn tại, xóa trigger cũ và tạo lại...");

            var oldTriggerKey = new TriggerKey("SyncPhimJob-trigger");
            await scheduler.UnscheduleJob(oldTriggerKey);

            var newTrigger = TriggerBuilder.Create()
                .ForJob(jobDetail)
                .WithIdentity("SyncPhimJob-trigger")
                //.StartNow() // có thể đổi sang .StartAt(DateBuilder.FutureDate(...)) nếu muốn delay, hiện tại vừa khởi chạy project là chạy bot ngay lập từc và lập lại sau số giờ trong config
                .StartAt(DateTimeOffset.Now.AddHours(botConfig.IntervalHours)) //Bot khởi chạy theo sau giờ đã config
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(botConfig.IntervalHours)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(newTrigger);

            Console.WriteLine($"✅ Job rescheduled thành công. Lần chạy tiếp theo: {newTrigger.GetNextFireTimeUtc()?.ToLocalTime()}");
        }
        else
        {
            var trigger = TriggerBuilder.Create()
                .ForJob(jobDetail)
                .WithIdentity("SyncPhimJob-trigger")
                .StartAt(DateTimeOffset.Now.AddHours(botConfig.IntervalHours))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(botConfig.IntervalHours)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            Console.WriteLine($"🕐 Lên lịch job lần đầu, chạy sau {botConfig.IntervalHours} giờ ({trigger.GetNextFireTimeUtc()?.ToLocalTime()})");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Cho phép FE gọi api
app.UseCors("AllowFrontend");

app.AuthAuthService(); // AuthService
app.UseUserService(); // UserService
app.FilmFilmService(); // FilmService
app.MembershipService(); // MembershipService

// Cho phép phục vụ file tĩnh từ wwwroot
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
