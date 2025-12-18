using GienPhim_Services.MembershipService.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using GienPhim_Services.MembershipService.Infrastructure.Data.HttpClients;
using GienPhim_Services.MembershipService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.MembershipService.Infrastructure.Repositories;
using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Application.Orchestrators;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;
using GienPhim_Services.MembershipService.Domain.Services;
using GienPhim_Services.MembershipService.Application.Services;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Application.Mappers;
using GienPhim_Services.MembershipService.Application.Factories;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Infrastructure.HttpClients.Interfaces;
using GienPhim_Services.MembershipService.Infrastructure.HttpClients;


namespace GienPhim_Services.MembershipService
{
    public static class MembershipServiceConfig
    {
        public static IServiceCollection AddMembershipService(this IServiceCollection services, IConfiguration config)
        {
            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            // SERVICE
            services.AddScoped<IMembershipPlanService, MembershipPlanService>();
            services.AddScoped<IPlanOptionService, PlanOptionService>();
            services.AddScoped<IUserMembershipService, UserMembershipService>();

            // FACTORIES
            services.AddScoped<IMembershipPlanFactory, MembershipPlanFactory>();
            services.AddScoped<IPlanOptionFactory, PlanOptionFactory>();
            services.AddScoped<IUserMembershipFactory, UserMembershipFactory>();

            // MAPPERS
            services.AddScoped<IMembershipPlanResponseMapper, MembershipPlanResponseMapper>();
            services.AddScoped<IPlanOptionResponseMapper, PlanOptionResponseMapper>();
            services.AddScoped<IUserMembershipResponseMapper, UserMembershipResponseMapper>();

            // ORCHESTRATORS
            services.AddScoped<IMembershipPlanOrchestrator, MembershipPlanOrchestrator>();
            services.AddScoped<IPlanOptionOrchestrator, PlanOptionOrchestrator>();
            services.AddScoped<IUserMembershipOrchestrator, UserMembershipOrchestrator>();

            // VALIRATORS
            // Dịch vụ validation chung
            /*services.AddScoped<IValidationService, ValidationService>();*/ // UserService

            // Đăng ký tất cả validators tự động
            //services.Scan(scan => scan
            //    .FromAssemblies(typeof(RegisterUserValidator).Assembly) // Quét tất cả các lớp trong assembly chứa RegisterUserValidator 
            //    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))// Đăng ký tất cả các lớp triển khai IValidator<>
            //    .AsImplementedInterfaces()// Đăng ký chúng theo giao diện mà chúng triển khai
            //    .WithScopedLifetime()); // Sử dụng phạm vi Scoped cho các dịch vụ này

            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // DOMAIN SERVICES
            services.AddScoped<IMembershipPlanDomainService, MembershipPlanDomainService>();
            services.AddScoped<IPlanOptionDomainService, PlanOptionDomainService>();
            services.AddScoped<IUserMembershipDomainService, UserMembershipDomainService>();

            //MAPPERS
            services.AddScoped<IPaginationMapper, PaginationMapper>();

            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/
            //CONFIG

            // HTTP Client
            services.AddHttpClient<IUserServiceClient, UserServiceClient>("Membership_UserServiceClient", (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var userServiceUrl = config["ServiceUrls:UserService"];
                client.BaseAddress = new Uri(userServiceUrl);
            });

            services.AddHttpClient<IWalletServiceClient, WalletServiceClient>("Membership_WalletClient", (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();// URL của UserService
                var walletServiceUrl = config["ServiceUrls:WalletService"];

                client.BaseAddress = new Uri(walletServiceUrl);
            });

            // Configure database contexts
            services.AddDbContext<MembershipDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("MembershipDb")));

            // REPOSITORIES
            services.AddScoped<IMembershipPlanRepository, MembershipPlanRepository>();
            services.AddScoped<IPlanOptionRepository, PlanOptionRepository>();
            services.AddScoped<IUserMembershipRepository, UserMembershipRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Unit of Work
            return services;
        }
        public static IApplicationBuilder MembershipService(this IApplicationBuilder app)
        {
            // Đăng ký middleware riêng của UserService
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
 