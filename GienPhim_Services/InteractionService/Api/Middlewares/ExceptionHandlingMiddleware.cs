using GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate;
using GienPhim_Services.InteractionService.Domain.Exceptions.Expired;
using GienPhim_Services.InteractionService.Domain.Exceptions.Permission;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Domain.Exceptions.State;
using GienPhim_Services.InteractionService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.InteractionService.Api.Middlewares
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

            // Chatgroup
            { typeof(ChatGroupNotFoundException), StatusCodes.Status404NotFound },
            { typeof(DuplicateChatGroupException), StatusCodes.Status409Conflict },
            { typeof(ChatGroupAlreadyClosedException), StatusCodes.Status500InternalServerError },
            { typeof(ChatGroupExpiredException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidChatGroupException), StatusCodes.Status500InternalServerError },
            { typeof(NotChatGroupOwnerException), StatusCodes.Status500InternalServerError },

            // GroupMember
            { typeof(DuplicateGroupMemberException), StatusCodes.Status409Conflict },
            { typeof(InvalidGroupMemberException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidGroupMemberException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidGroupMemberException), StatusCodes.Status500InternalServerError },

        };

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/ChatGroup") &&
                !context.Request.Path.StartsWithSegments("/api/GroupMember") &&
                !context.Request.Path.StartsWithSegments("/api/JoinRequest") &&
                !context.Request.Path.StartsWithSegments("/api/ChatMessage"))
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
