using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Services;
using GienPhim_Services.UserService.Application.Services;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Services.Interfaces;
using GienPhim_Services.UserService.Domain.Services;
using GienPhim_Services.UserService.Infrastructure.Config;
using GienPhim_Services.UserService.Infrastructure.Data;
using GienPhim_Services.UserService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GienPhim_Services.UserService.Application.Validators.Users;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Factories.Interfaces;
using GienPhim_Services.UserService.Domain.Factories;
using GienPhim_Services.UserService.Application.Interfaces.Orchestrators;
using GienPhim_Services.UserService.Application.Orchestrators;
using GienPhim_Services.UserService.Api.Middlewares;
using GienPhim_Services.UserService.Application.Interfaces.Mappers;
using GienPhim_Services.UserService.Application.Mappers;

namespace GienPhim_Services.UserService
{
    public static class UserServiceConfig
    {
        public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration config)
        {

            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            services.AddScoped<IUserService, Application.Services.UserService>(); // User Service
            services.AddScoped<IRoleService, RoleService>(); // Role Service

            // VALIRATORS
            // Dịch vụ validation chung
            services.AddScoped<IValidationService, ValidationService>(); // UserService

            // ORCHESTRATORS
            services.AddScoped<IUserOrchestrator, UserOrchestrator>(); // User Orchestrator
            services.AddScoped<IRoleOrchestrator, RoleOrchestrator>(); // Role Orchestrator

            // FACTORIES    
            services.AddScoped<Application.Interfaces.Factories.IUserFactory,Application.Factories.UserFactory>(); 
            services.AddScoped<Application.Interfaces.Factories.IRoleFactory, Application.Factories.RoleFactory>(); 

            // MAPPERS
            services.AddScoped<IUserResponseMapper, UserResponseMapper>(); // User Mapper

            // Đăng ký tất cả validators tự động
            services.Scan(scan => scan
                .FromAssemblies(typeof(RegisterUserValidator).Assembly) // Quét tất cả các lớp trong assembly chứa RegisterUserValidator 
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))// Đăng ký tất cả các lớp triển khai IValidator<>
                .AsImplementedInterfaces()// Đăng ký chúng theo giao diện mà chúng triển khai
                .WithScopedLifetime()); // Sử dụng phạm vi Scoped cho các dịch vụ này


            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // FACTORIES
            services.AddScoped<IUserFactory, UserFactory>(); // Tạo username ngẫu nhiên
            services.AddScoped<IRoleFactory, RoleFactory>(); // Tạo role mới

            // DOMAINSERVICES
            services.AddScoped<IUserDomainService, UserDomainService>(); // User Domain Service
            services.AddScoped<IRoleDomainService, RoleDomainService>(); // Role Domain Service

            // GENERATE USERNAME GUID
            services.AddScoped<IGuidUserNameGenerator, GuidUserNameGenerator>(); // Tạo username ngẫu nhiên


            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/
            //CONFIG
            // Config ánh xạ UserSetting vs model UserSetting
            services.Configure<UserSettings>(config.GetSection("UserSettings"));

            // Configure database contexts
            // UserService
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("UserDb")));

            // IDENTITY
            // Cấu hình Identity để quản lý người dùng (User) và vai trò (Role).
            services.AddIdentityCore<User>(options => { })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            // REPOSITORIES
            services.AddScoped<IRoleRepository, RoleRepository>(); // Role Repository
            services.AddScoped<IUserRepository, UserRepository>(); // User Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Base Repository

            return services;
        }

        public static IApplicationBuilder AuthAuthService(this IApplicationBuilder app)
        {
            // Đăng ký middleware riêng của UserService
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
