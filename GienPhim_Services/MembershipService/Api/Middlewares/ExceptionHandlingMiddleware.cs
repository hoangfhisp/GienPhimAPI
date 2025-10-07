using GienPhim_Services.MembershipService.Domain.Exceptions.Duplicates;
using GienPhim_Services.MembershipService.Domain.Exceptions.Failed;
using GienPhim_Services.MembershipService.Domain.Exceptions.Invalid;
using GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.MembershipService.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private static readonly Dictionary<Type, int> ExceptionStatusMap = new()
        {
            // { typeof(CreateUserFailedException), StatusCodes.Status500InternalServerError }

            // MembershipPlan
            { typeof(DuplicateMembershipPlanException), StatusCodes.Status500InternalServerError },
            { typeof(MembershipPlanFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidMembershipPlanException), StatusCodes.Status500InternalServerError },
            { typeof(MembershipPlanNotFoundException), StatusCodes.Status500InternalServerError },

            // PlanOption
            { typeof(DuplicatePlanOptionException), StatusCodes.Status500InternalServerError },
            { typeof(PlanOptionFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidPlanOptionException), StatusCodes.Status500InternalServerError },
            { typeof(PlanOptionNotFoundException), StatusCodes.Status500InternalServerError },

            // UserMembership
            { typeof(DuplicateUserMembershipException), StatusCodes.Status500InternalServerError },
            { typeof(UserMembershipFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidUserMembershipException), StatusCodes.Status500InternalServerError },
            { typeof(UserMembershipNotFoundException), StatusCodes.Status500InternalServerError },
        };

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/MembershipPlan") &&
                !context.Request.Path.StartsWithSegments("/api/PlanOption") &&
                !context.Request.Path.StartsWithSegments("/api/UserMembership"))
            {
                await _next(context);
                return;
            }
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Domain error occurred");

                int statusCode = ExceptionStatusMap.TryGetValue(ex.GetType(), out var code)
                    ? code
                    : StatusCodes.Status400BadRequest;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
            catch (BusinessException ex) // lỗi nghiệp vụ
            {
                _logger.LogWarning(ex, "Business error occurred.");

                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    Status = false,
                    Msg = ex.Message
                });
            }
            catch (Exception ex) // lỗi khác
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    Status = false,
                    Msg = $"Đã xảy ra lỗi hệ thống, vui lòng thử lại sau. {ex}"
                });
            }
        }
    }
}
