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
using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients;

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
            services.AddScoped<IFeedBackService, FeedBackService>();

            // FACTORIES
            services.AddScoped<IChatGroupFactory, ChatGroupFactory>();
            services.AddScoped<IGroupMemberFactory, GroupMemberFactory>();
            services.AddScoped<IJoinRequestFactory, JoinRequestFactory>();
            services.AddScoped<IChatMessageFactory, ChatMessageFactory>();
            services.AddScoped<IFeedBackFactory, FeedBackFactory>();

            // MAPPERS
            services.AddScoped<IChatGroupResponseMapper, ChatGroupResponseMapper>();
            services.AddScoped<IGroupMemberResponseMapper, GroupMemberResponseMapper>();
            services.AddScoped<IJoinRequestResponseMapper, JoinRequestResponseMapper>();
            services.AddScoped<IChatMessageResponseMapper, ChatMessageResponseMapper>();
            services.AddScoped<IFeedBackResponseMapper, FeedBackResponseMapper>();
            services.AddScoped<IPaginationMapper, PaginationMapper>();
            // ORCHESTRATORS
            services.AddScoped<IChatGroupOrchestrator, ChatGroupOrchestrator>();
            services.AddScoped<IGroupMemberOrchestrator, GroupMemberOrchestrator>();
            services.AddScoped<IJoinRequestOrchestrator, JoinRequestOrchestrator>();
            services.AddScoped<IChatMessageOrchestrator, ChatMessageOrchestrator>();
            services.AddScoped<IFeedBackOrchestrator, FeedBackOrchestrator>();

            /*-----------------------------------------DOMAIN--------------------------------------------*/
            // DOMAIN SERVICES
            services.AddScoped<IChatGroupDomainService, ChatGroupDomainService>();
            services.AddScoped<IGroupMemberDomainService, GroupMemberDomainService>();
            services.AddScoped<IJoinRequestDomainService, JoinRequestDomainService>();
            services.AddScoped<IChatMessageDomainService, ChatMessageDomainService>();
            services.AddScoped<IFeedBackDomainService, FeedBackDomainService>();
 
            

            /*-----------------------------------------INFRASTRUCTURE--------------------------------------------*/
            //CONFIG

            // HTTP Client
            services.AddHttpClient<IUserServiceClient, UserServiceClient>("Interaction_UserClient", (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();// URL của UserService
                var userServiceUrl = config["ServiceUrls:UserService"];

                client.BaseAddress = new Uri(userServiceUrl);
            });

            services.AddHttpClient<IPhimServiceClient, PhimServiceClient>("Interaction_FilmClient", (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();// URL của UserService
                var FilmService = config["ServiceUrls:FilmService"];

                client.BaseAddress = new Uri(FilmService);
            });

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
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();

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

