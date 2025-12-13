using GienPhim_Services.InteractionService.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using GienPhim_Services.InteractionService.Infrastructure.Data;
//using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients;
//using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.InteractionService.Infrastructure.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Orchestrators;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using GienPhim_Services.InteractionService.Domain.Services;
using GienPhim_Services.InteractionService.Application.Services;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Mappers;
using GienPhim_Services.InteractionService.Application.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Infrastructure.Services;

namespace GienPhim_Services.InteractionService
{
    public static class InteractionServiceConfig
    {
        public static IServiceCollection AddInteractionService(this IServiceCollection services, IConfiguration config)
        {
            /*-----------------------------------------APPLICATION--------------------------------------------*/
            // SERVICES
            services.AddScoped<IChatGroupService, ChatGroupService>();
            services.AddScoped<IGroupMemberService, GroupMemberService>();
            services.AddScoped<IJoinRequestService, JoinRequestService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();

            // FACTORIES
            services.AddScoped<IChatGroupFactory, ChatGroupFactory>();
            services.AddScoped<IGroupMemberFactory, GroupMemberFactory>();
            services.AddScoped<IJoinRequestFactory, JoinRequestFactory>();
            services.AddScoped<IChatMessageFactory, ChatMessageFactory>();

            // MAPPERS
            services.AddScoped<IChatGroupResponseMapper, ChatGroupResponseMapper>();
            services.AddScoped<IGroupMemberResponseMapper, GroupMemberResponseMapper>();
            services.AddScoped<IJoinRequestResponseMapper, JoinRequestResponseMapper>();
            services.AddScoped<IChatMessageResponseMapper, ChatMessageResponseMapper>();

            // ORCHESTRATORS
            services.AddScoped<IChatGroupOrchestrator, ChatGroupOrchestrator>();
            services.AddScoped<IGroupMemberOrchestrator, GroupMemberOrchestrator>();
            services.AddScoped<IJoinRequestOrchestrator, JoinRequestOrchestrator>();
            services.AddScoped<IChatMessageOrchestrator, ChatMessageOrchestrator>();

            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // DOMAIN SERVICES
            services.AddScoped<IChatGroupDomainService, ChatGroupDomainService>();
            services.AddScoped<IGroupMemberDomainService, GroupMemberDomainService>();
            services.AddScoped<IJoinRequestDomainService, JoinRequestDomainService>();
            services.AddScoped<IChatMessageDomainService, ChatMessageDomainService>();

            //MAPPERS
            services.AddScoped<IPaginationMapper, PaginationMapper>();

            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/
            //CONFIG

            // HTTP Client

            // Configure database contexts
            services.AddDbContext<InteractionDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("InteractionDb")));

            // SERVICE
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, HttpContextUserService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // REPOSITORIES
            services.AddScoped<IChatGroupRepository, ChatGroupRepository>();
            services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            services.AddScoped<IJoinRequestRepository, JoinRequestRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Unit of Work
            return services;
        }

        public static IApplicationBuilder InteractionService(this IApplicationBuilder app)
        {
            // Đăng ký middleware riêng của UserService
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}

