using GienPhim_Services.AuthService.Application.Exceptions.Invalid;
using GienPhim_Services.AuthService.Application.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Factories;
using GienPhim_Services.AuthService.Infrastructure.Data.ExternalAuth;
using GienPhim_Services.AuthService.Application.Services;
using GienPhim_Services.AuthService.Application.Interfaces.Validators;
using GienPhim_Services.AuthService.Api.Middlewares;
using GienPhim_Services.AuthService.Application.Interfaces.Orchestrators;
using GienPhim_Services.AuthService.Application.Orchestrators;
using GienPhim_Services.AuthService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.AuthService.Infrastructure.Data.HttpClients;
using GienPhim_Services.AuthService.Application.Interfaces.Mappers;
using GienPhim_Services.AuthService.Application.Mappers;
using GienPhim_Services.AuthService.Domain.Services;
using GienPhim_Services.AuthService.Application.Options;
using GienPhim_Services.AuthService.Infrastructure.Services;
using GienPhim_Services.AuthService.Application.Interfaces.Repositories;
using GienPhim_Services.AuthService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GienPhim_Services.UserService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GienPhim_Services.AuthService.Infrastructure;
using GienPhim_Services.AuthService.Domain.Interfaces.Factories;
using GienPhim_Services.AuthService.Domain.Interfaces.Services;
using GienPhim_Services.AuthService.Domain.Interfaces.Jwt;
using GienPhim_Services.AuthService.Application.Interfaces.Templates;
using GienPhim_Services.AuthService.Application.Templates;
using GienPhim_Services.AuthService.Infrastructure.Security;
using GienPhim_Services.AuthService.Application.Interfaces.Factories;
using GienPhim_Services.AuthService.Application.Factories;

namespace GienPhim_Services.AuthService
{
    public static class AuthServiceConfig
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration config)
        {

            /*-----------------------------------APPLICATION----------------------------------------*/
            //SERVICE
            services.AddScoped<IAuthService, Application.Services.AuthService>(); // Auth Service

            ///VALIRATORS
            // Dịch vụ validation chung
            services.AddScoped<IValidationService, ValidationService>(); // AuthService

            //ORCHESTRATORS
            services.AddScoped<IAuthOrchestrator, AuthOrchestrator>(); // Auth Orchestrator

            // Đăng ký tất cả validators tự động
            //AuthService
            services.Scan(scan => scan
                .FromAssemblies(typeof(InvalidTokenException).Assembly) // Quét tất cả các lớp trong assembly chứa RegisterUserValidator 
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))// Đăng ký tất cả các lớp triển khai IValidator<>
                .AsImplementedInterfaces()// Đăng ký chúng theo giao diện mà chúng triển khai
                .WithScopedLifetime()); // Sử dụng phạm vi Scoped cho các dịch vụ này

            // FACOTRY
            services.AddScoped<ITokenFactory, TokenFactory>(); // Tạo mã OTP

            // MAPPER
            services.AddScoped<IUserClaimsMapper, UserClaimsMapper>(); 
            services.AddScoped<IExternalUserMapper, ExternalUserMapper>();
            services.AddScoped<IUserHttpMapper, UserHttpMapper>();

            // Templates
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            // Options
            services.Configure<EmailOptions>(
                config.GetSection("EmailSettings"));
            services.Configure<AuthOptions>(
                config.GetSection("AuthOptions"));

            /*-------------------------------------DOMAIN----------------------------------------*/
            // FACTORIES
            services.AddScoped<IAuthClaimsFactory, AuthClaimsFactory>(); // Tạo claim cho token
            services.AddScoped<IOtpFactory, OtpFactory>(); // Tạo mã OTP


            // DOMAINSERVICES
            services.AddScoped<IAuthDomainService, AuthDomainService>(); // AuthDomainService
            services.AddScoped<IOtpDomainService, OtpDomainService>(); // Tạo và xác thực token

            /*-----------------------------------INFRASTRUCTURE----------------------------------------*/

            // Configure database contexts
            // UserService
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("AuthDb")));

            // ExternalAuth
            services.AddScoped<IExternalAuthProvider, GoogleAuthProvider>(); // Đăng nhập bằng google  

            // Repositories
            services.AddScoped<IOtpRepository, InMemoryOtpRepository>(); // Token Repository
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>(); // RefreshToken Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Base Repository
            services.AddMemoryCache();
            // Services
            services.AddScoped<IEmailService, SmtpEmailService>(); // Gửi email
            services.AddScoped<IHttpUserContextAccessor, HttpUserContextAccessor>(); // Quản lý token bị thu hồi

            // HTTP Client
            services.AddHttpClient<IUserServiceClient, UserServiceClient>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();// URL của UserService
                var userServiceUrl = config["ServiceUrls:UserService"];

                client.BaseAddress = new Uri(userServiceUrl);
            });

            // Đăng ký trực tiếp JwtSettings để inject từ appsetting vào
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtOptions>>().Value);

            // CONNET
            // Lấy danh sách origin từ appsettings với key ConnetAllowedOrigins
            var allowedOrigins = config.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });


            // Security
            services.AddScoped<IJwtProvider, JwtUtils>(); // Jwt Utils
            services.AddScoped<ITokenGenerator, CryptoTokenGenerator>(); // Mã hóa mật khẩu

            // Authentication JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = config.GetSection("AuthOptions:Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Ngăn chặn response mặc định
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            status = false,
                            msg = "Bạn chưa đăng nhập hoặc token không hợp lệ"
                        });

                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            status = false,
                            msg = "Bạn không có quyền truy cập tài nguyên này"
                        });

                        return context.Response.WriteAsync(result);
                    }
                };
            });

            // Đăng ký HttpContextAccessor để sử dụng trong các lớp dịch vụ và kho lưu trữ.
            services.AddHttpContextAccessor();

            return services;
        }
        public static IApplicationBuilder UseUserService(this IApplicationBuilder app)
        {
            // Đăng ký middleware riêng của UserService
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }

    }
}
