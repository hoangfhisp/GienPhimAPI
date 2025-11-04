using GienPhim_Services.AuthService;
using GienPhim_Services.FilmService;
using GienPhim_Services.Filters;
using GienPhim_Services.InteractionService.Data;
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


//ADD MICROSERVICE
builder.Services.AddUserService(builder.Configuration); // UserService
builder.Services.AddAuthService(builder.Configuration); // AuthService
builder.Services.AddFilmService(builder.Configuration); // FilmService
builder.Services.AddMembershipService(builder.Configuration); // MembershipService

//// Đăng ký Quartz job cho đồng bộ phim
//builder.Services.AddQuartz(q =>
//{
//    var jobKey = new JobKey("SyncPhimJob");

//    // Đăng ký job
//    q.AddJob<SyncPhimJob>(opts => opts.WithIdentity(jobKey));

//    // Đặt lịch chạy — ví dụ: mỗi 3 giờ 1 lần
//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("SyncPhimJob-trigger")
//        .WithSimpleSchedule(x => x
//            .WithIntervalInHours(3)   // ⏱ thay đổi nếu muốn (ví dụ .WithIntervalInMinutes(30))
//            .RepeatForever()));
//});

//// Cho phép Quartz chạy nền khi app khởi động -> tự động lấy api mỗi khi chạy project, sau 3 giờ tự động lấy api tiếp
//builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

// Chỉ chạy sau 3 giờ kể từ khi backend khởi động
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("SyncPhimJob");
    q.AddJob<SyncPhimJob>(opts => opts.WithIdentity(jobKey));

    // Trigger: bắt đầu sau 3 giờ, lặp lại mỗi 3 giờ
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SyncPhimJob-trigger")
        .StartAt(DateBuilder.FutureDate(3, IntervalUnit.Hour)) // ⏳ đợi 3 tiếng
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(3)
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

//// Lịch cố định hằng ngày chạy vào 3 giờ sáng mỗi ngày
//builder.Services.AddQuartz(q =>
//{
//    var jobKey = new JobKey("SyncPhimJob");
//    q.AddJob<SyncPhimJob>(opts => opts.WithIdentity(jobKey));

//    // Trigger: chạy vào 03:00 sáng mỗi ngày
//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("SyncPhimJob-trigger")
//        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(3, 0)) // 🕒 3:00 AM
//        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))); // 🇻🇳 múi giờ Việt Nam
//});

//builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

var app = builder.Build();
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
