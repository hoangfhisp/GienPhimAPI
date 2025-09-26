using GienPhim_Services.FilmService.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Infrastructure.Data;
using GienPhim_Services.FilmService.Infrastructure.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Orchestrators;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using GienPhim_Services.FilmService.Domain.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Mappers;
using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Validators.DaoDien;
using GienPhim_Services.FilmService.Application.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;

namespace GienPhim_Services.FilmService
{
    public static class FilmServiceConfig
    {
        public static IServiceCollection AddFilmService(this IServiceCollection services, IConfiguration config)
        {
            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            // SERVICE
            services.AddScoped<IFilmService, FilmService.Application.Services.FilmService>(); // Film Service
            services.AddScoped<IDaoDienService, DaoDienService>(); // Đạo diễn Service                                                                            

            // Đạo diễn
            services.AddScoped<IValidator<DaoDienCreateDTO>, CreateDaoDienValidator>();

            // FACTORIES
            services.AddScoped<IDaoDienFactory, DaoDienFactory>(); // Đạo diễn Factory

            // MAPPERS
            services.AddScoped<IDaoDienResponseMapper, DaoDienResponseMapper>(); // Đạo diễn Mapper

            // ORCHESTRATORS
            services.AddScoped<IFilmOrchestrator,FilmOrchestrator>(); // Film Orchestrator
            services.AddScoped<IDaoDienOrchestrator, DaoDienOrchestrator>(); // Đạo diễn Orchestrator

            // VALIRATORS
            // Dịch vụ validation chung
            services.AddScoped<IValidationService, ValidationService>(); // UserService

            // Đăng ký tất cả validators tự động
            //services.Scan(scan => scan
            //    .FromAssemblies(typeof(RegisterUserValidator).Assembly) // Quét tất cả các lớp trong assembly chứa RegisterUserValidator 
            //    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))// Đăng ký tất cả các lớp triển khai IValidator<>
            //    .AsImplementedInterfaces()// Đăng ký chúng theo giao diện mà chúng triển khai
            //    .WithScopedLifetime()); // Sử dụng phạm vi Scoped cho các dịch vụ này

            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // DOMAIN SERVICES
            services.AddScoped<IFilmDomainService, FilmDomainService>();
            services.AddScoped<IDaoDienDomainService, DaoDienDomainService>();

            //MAPPERS
            services.AddScoped<IPaginationMapper,PaginationMapper>();
            services.AddScoped<IDaoDienResponseMapper, DaoDienResponseMapper>();

            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/
            //CONFIG
            // Configure database contexts
            services.AddDbContext<FilmDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("FilmDb")));

            // REPOSITORIES
            services.AddScoped<IDaoDienRepository, DaoDienRepository>(); // Đạo Diễn
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Unit of Work
            return services;
        }
        public static IApplicationBuilder FilmFilmService(this IApplicationBuilder app)
        {
            // Đăng ký middleware riêng của UserService
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
 