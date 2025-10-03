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
using GienPhim_Services.FilmService.Application.Validators.DienVien;
using GienPhim_Services.FilmService.Application.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Validators.TheLoai;
using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Validators.TheTag;

namespace GienPhim_Services.FilmService
{
    public static class FilmServiceConfig
    {
        public static IServiceCollection AddFilmService(this IServiceCollection services, IConfiguration config)
        {
            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            // SERVICE
            services.AddScoped<IDaoDienService, DaoDienService>(); // Đạo diễn Service                                                                            
            services.AddScoped<IDienVienService, DienVienService>(); // Diễn viên Service
            services.AddScoped<ITheLoaiService, TheLoaiService>(); // Thể loại Service
            services.AddScoped<ITheTagService, TheTagService>(); // Thẻ Tag Service
            services.AddScoped<INgonNguService, NgonNguService>(); // Ngôn ngữ Service
            services.AddScoped<IQuocGiaService, QuocGiaService>(); // Quốc gia Service
            services.AddScoped<IPhimService, PhimService>(); // Phim Service
            services.AddScoped<IDaoDienCuaPhimService, DaoDienCuaPhimService>(); // Đạo diễn của phim Service
            services.AddScoped<IDienVienCuaPhimService, DienVienCuaPhimService>(); // Diễn viên của phim Service
            services.AddScoped<IPhanLoaiTheLoaiService, PhanLoaiTheLoaiService>(); // Phân loại thể loại Service
            services.AddScoped<IPhanLoaiTheTagService, PhanLoaiTheTagService>(); // Phân loại thẻ tag Service
            services.AddScoped<ITapPhimService, TapPhimService>(); // Tập phim Service
            services.AddScoped<IFilmViewService, FilmViewService>(); // Lượt xem phim Service

            // Đạo diễn
            services.AddScoped<IValidator<DaoDienCreateDTO>, CreateDaoDienValidator>(); // Validator tạo đạo diễn
            services.AddScoped<IValidator<DienVienCreateDTO>, CreateDienVienValidator>(); // Validator cập nhật đạo diễn
            services.AddScoped<IValidator<TheLoaiCreateDTO>, CreateTheLoaiValidator>(); // Validator cập nhật đạo diễn
            services.AddScoped<IValidator<TheTagCreateDTO>, CreateTheTagValidator>(); // Validator cập nhật đạo diễn

            // FACTORIES
            services.AddScoped<IDaoDienFactory, DaoDienFactory>(); // Đạo diễn Factory
            services.AddScoped<IDienVienFactory, DienVienFactory>(); // Diễn viên Factory
            services.AddScoped<ITheLoaiFactory, TheLoaiFactory>(); // Thể loại Factory
            services.AddScoped<ITheTagFactory, TheTagFactory>(); // Thẻ Tag Factory
            services.AddScoped<INgonNguFactory, NgonNguFactory>(); // Ngôn ngữ Factory
            services.AddScoped<IQuocGiaFactory, QuocGiaFactory>(); // Quốc gia Factory
            services.AddScoped<IPhimFactory, PhimFactory>(); // Phim Factory
            services.AddScoped<IDaoDienCuaPhimFactory, DaoDienCuaPhimFactory>(); // Đạo diễn Factory
            services.AddScoped<IDienVienCuaPhimFactory, DienVienCuaPhimFactory>(); // Diễn viên Factory
            services.AddScoped<IPhanLoaiTheLoaiFactory, PhanLoaiTheLoaiFactory>(); // Phân loại thể loại Factory
            services.AddScoped<IPhanLoaiTheTagFactory, PhanLoaiTheTagFactory>(); // Phân loại thẻ tag Factory
            services.AddScoped<ITapPhimFactory, TapPhimFactory>(); // Tập phim Factory
            services.AddScoped<IFilmViewFactory, FilmViewFactory>(); // Lượt xem phim Factory

            // MAPPERS
            services.AddScoped<IDaoDienResponseMapper, DaoDienResponseMapper>(); // Đạo diễn Mapper
            services.AddScoped<IDienVienResponseMapper, DienVienResponseMapper>(); // Diễn viên Mapper
            services.AddScoped<ITheLoaiResponseMapper, TheLoaiResponseMapper>(); // Thể loại Mapper
            services.AddScoped<ITheTagResponseMapper, TheTagResponseMapper>(); // Thẻ Tag Mapper
            services.AddScoped<INgonNguResponseMapper, NgonNguResponseMapper>(); // Ngôn ngữ Mapper
            services.AddScoped<IQuocGiaResponseMapper, QuocGiaResponseMapper>(); // Quốc gia Mapper
            services.AddScoped<IPhimResponseMapper, PhimResponseMapper>(); // Phim Mapper
            services.AddScoped<IDaoDienCuaPhimResponseMapper, DaoDienCuaPhimResponseMapper>(); // Đạo diễn Mapper
            services.AddScoped<IDienVienCuaPhimResponseMapper, DienVienCuaPhimResponseMapper>(); // Diễn viên Mapper
            services.AddScoped<IPhanLoaiTheLoaiResponseMapper, PhanLoaiTheLoaiResponseMapper>(); // Phân loại thể loại Mapper
            services.AddScoped<IPhanLoaiTheTagResponseMapper, PhanLoaiTheTagResponseMapper>(); // Phân loại thẻ tag Mapper
            services.AddScoped<ITapPhimResponseMapper, TapPhimResponseMapper>(); // Tập phim Mapper
            services.AddScoped<IFilmViewResponseMapper, FilmViewResponseMapper>(); // Lượt xem phim Mapper

            // ORCHESTRATORS
            services.AddScoped<IDaoDienOrchestrator, DaoDienOrchestrator>(); // Đạo diễn Orchestrator
            services.AddScoped<IDienVienOrchestrator, DienVienOrchestrator>(); // Diễn viên Orchestrator
            services.AddScoped<ITheLoaiOrchestrator, TheLoaiOrchestrator>(); // Thể loại Orchestrator
            services.AddScoped<ITheTagOrchestrator, TheTagOrchestrator>(); // Thẻ Tag Orchestrator
            services.AddScoped<INgonNguOrchestrator, NgonNguOrchestrator>(); // Ngôn ngữ Orchestrator
            services.AddScoped<IQuocGiaOrchestrator, QuocGiaOrchestrator>(); // Quốc gia Orchestrator
            services.AddScoped<IPhimOrchestrator, PhimOrchestrator>(); // Phim Orchestrator
            services.AddScoped<IDaoDienCuaPhimOrchestrator, DaoDienCuaPhimOrchestrator>(); // Đạo diễn Orchestrator
            services.AddScoped<IDienVienCuaPhimOrchestrator, DienVienCuaPhimOrchestrator>(); // Diễn viên Orchestrator
            services.AddScoped<IPhanLoaiTheLoaiOrchestrator, PhanLoaiTheLoaiOrchestrator>(); // Phân loại thể loại Orchestrator
            services.AddScoped<IPhanLoaiTheTagOrchestrator, PhanLoaiTheTagOrchestrator>(); // Phân loại thẻ tag Orchestrator
            services.AddScoped<ITapPhimOrchestrator, TapPhimOrchestrator>(); // Tập phim Orchestrator
            services.AddScoped<IFilmViewOrchestrator, FilmViewOrchestrator>(); // Lượt xem phim Orchestrator

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
            services.AddScoped<IDaoDienDomainService, DaoDienDomainService>();
            services.AddScoped<IDienVienDomainService, DienVienDomainService>();
            services.AddScoped<ITheLoaiDomainService, TheLoaiDomainService>();
            services.AddScoped<ITheTagDomainService, TheTagDomainService>();
            services.AddScoped<INgonNguDomainService, NgonNguDomainService>();
            services.AddScoped<IQuocGiaDomainService, QuocGiaDomainService>();
            services.AddScoped<IPhimDomainService, PhimDomainService>();
            services.AddScoped<IDaoDienCuaPhimDomainService, DaoDienCuaPhimDomainService>();
            services.AddScoped<IDienVienCuaPhimDomainService, DienVienCuaPhimDomainService>();
            services.AddScoped<IPhanLoaiTheLoaiDomainService, PhanLoaiTheLoaiDomainService>();
            services.AddScoped<IPhanLoaiTheTagDomainService, PhanLoaiTheTagDomainService>();
            services.AddScoped<ITapPhimDomainService, TapPhimDomainService>();
            services.AddScoped<IFilmViewDomainService, FilmViewDomainService>();

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
            services.AddScoped<IDienVienRepository, DienVienRepository>(); // Diễn Viên
            services.AddScoped<ITheLoaiRepository, TheLoaiRepository>(); // Thể Loại
            services.AddScoped<ITheTagRepository, TheTagRepository>(); // Thẻ Tag
            services.AddScoped<INgonNguRepository, NgonNguRepository>(); // Ngôn Ngữ
            services.AddScoped<IQuocGiaRepository, QuocGiaRepository>(); // Quốc Gia
            services.AddScoped<IPhimRepository, PhimRepository>(); // Phim
            services.AddScoped<IDaoDienCuaPhimRepository, DaoDienCuaPhimRepository>(); // Đạo Diễn Của Phim
            services.AddScoped<IDienVienCuaPhimRepository, DienVienCuaPhimRepository>(); // Diễn Viên Của Phim
            services.AddScoped<IPhanLoaiTheLoaiRepository, PhanLoaiTheLoaiRepository>(); // Phân loại thể loại
            services.AddScoped<IPhanLoaiTheTagRepository, PhanLoaiTheTagRepository>(); // Phân loại thẻ tag
            services.AddScoped<ITapPhimRepository, TapPhimRepository>(); // Tập Phim
            services.AddScoped<IFilmViewRepository, FilmViewRepository>(); // Lượt Xem Phim
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
 