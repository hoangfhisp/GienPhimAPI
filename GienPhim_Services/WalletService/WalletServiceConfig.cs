using GienPhim_Services.WalletService.Api.Middlewares;
using GienPhim_Services.WalletService.Application.Exceptions.Duplicates;
using GienPhim_Services.WalletService.Application.Factories;
using GienPhim_Services.WalletService.Application.Interfaces.Factories;
using GienPhim_Services.WalletService.Application.Interfaces.Mappers;
using GienPhim_Services.WalletService.Application.Interfaces.Orchestrators;
using GienPhim_Services.WalletService.Application.Interfaces.Payments;
using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Application.Interfaces.Services;
using GienPhim_Services.WalletService.Application.Interfaces.Validators;
using GienPhim_Services.WalletService.Application.Mappers;
using GienPhim_Services.WalletService.Application.Orchestrators;
using GienPhim_Services.WalletService.Application.Services;
using GienPhim_Services.WalletService.Domain.Interfaces.Services;
using GienPhim_Services.WalletService.Domain.Services;
using GienPhim_Services.WalletService.Infrastructure.Data;
using GienPhim_Services.WalletService.Infrastructure.HttpClients;
using GienPhim_Services.WalletService.Infrastructure.HttpClients.Interfaces;
using GienPhim_Services.WalletService.Infrastructure.Payments.VnPay;
using GienPhim_Services.WalletService.Infrastructure.Repositories;
using GienPhim_Services.WalletService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.WalletService
{
    public static class WalletServiceConfig
    {
        public static IServiceCollection AddWalletService(this IServiceCollection services, IConfiguration config)
        {
            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            services.AddScoped<IWalletService,Application.Services.WalletService>(); // Wallet Service

            // FACTORIES
            services.AddScoped<IWalletFactory, WalletFactory>(); // Wallet Factory 
            services.AddScoped<IWalletTransactionFactory, WalletTransactionFactory>(); // Wallet Transaction Factory

            // ORCHESTRATOR
            services.AddScoped<IWalletOrchestrator, WalletOrchestrator>(); // Wallet Orchestrator

            // MAPPERS
            services.AddScoped<IWalletResponseMapper, WalletResponseMapper>(); // Wallet Mapper
            services.AddScoped<IPaginationMapper, PaginationMapper>(); // Pagination Mapper

            // Đăng ký tất cả validators tự động
            //AuthService
            services.Scan(scan => scan
                .FromAssemblies(typeof(DuplicateWalletUserIdException).Assembly) // Quét tất cả các lớp trong assembly chứa RegisterUserValidator 
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))// Đăng ký tất cả các lớp triển khai IValidator<>
                .AsImplementedInterfaces()// Đăng ký chúng theo giao diện mà chúng triển khai
                .WithScopedLifetime()); // Sử dụng phạm vi Scoped cho các dịch vụ này

            services.AddScoped<IValidationService, ValidationService>();
            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // DOMAINSERVICES
            services.AddScoped<IWalletDomainService, WalletDomainService>(); // Wallet Domain Service
            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/

            // Config DbContext
            services.AddDbContext<WalletDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("WalletDb")));
            services.Configure<VnPaySettings>(config.GetSection("Vnpay"));

            //Service
            services.AddScoped<IHttpUserContextAccessor, HttpUserContextAccessor>();
            services.AddScoped<IPaymentGateway, VnPayPaymentGateway>();


            // REPOSITORIES
            services.AddScoped<IWalletRepository, WalletRepository>(); // Wallet Repository
            services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>(); // Transaction Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Unit of Work

            // HTTP Client
            services.AddHttpClient<IUserServiceClient, UserServiceClient>("WalletService_UserClient", (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();// URL của UserService
                var userServiceUrl = config["ServiceUrls:UserService"];

                client.BaseAddress = new Uri(userServiceUrl);
            });
            return services;
        }

        public static IApplicationBuilder UserWalletService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
