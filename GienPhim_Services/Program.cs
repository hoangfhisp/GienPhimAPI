using GienPhim_Services.AuthService;
using GienPhim_Services.FilmService;
using GienPhim_Services.Filters;
using GienPhim_Services.InteractionService.Data;
using GienPhim_Services.MembershipService;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using GienPhim_Services.OrderService.Data;
using GienPhim_Services.PaymentService.Data;
using GienPhim_Services.UserService;
using GienPhim_Services.WalletService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


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
// OrderService
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));


//ADD MICROSERVICE
builder.Services.AddUserService(builder.Configuration); // UserService
builder.Services.AddAuthService(builder.Configuration); // AuthService
builder.Services.AddFilmService(builder.Configuration); // FilmService
builder.Services.AddMembershipService(builder.Configuration); // MembershipService
builder.Services.AddWalletService(builder.Configuration); // WalletService
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
app.UserWalletService(); // WalletService

// Cho phép phục vụ file tĩnh từ wwwroot
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
