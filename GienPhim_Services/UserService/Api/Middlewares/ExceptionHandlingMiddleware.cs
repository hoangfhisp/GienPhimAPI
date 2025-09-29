using GienPhim_Services.UserService.Domain.Exceptions.Duplicate;
using GienPhim_Services.UserService.Domain.Exceptions.Invalid;
using GienPhim_Services.UserService.shared.Exceptions;
using GienPhim_Services.UserService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.UserService.Api.Middlewares
{
    // Ngoại lệ trung gian
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Dictionary mapping exception type → HTTP status
        private static readonly Dictionary<Type, int> ExceptionStatusMap = new()
        {
            //Duplicate
            { typeof(DuplicateEmailException), StatusCodes.Status409Conflict },
            // Invalid
            { typeof(InvalidRoleNameNullException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserEmailException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserAuthIdException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserTypeLoginException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserIdException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserPhoneNumberException), StatusCodes.Status400BadRequest },
            { typeof(InvalidChangeEmailException), StatusCodes.Status400BadRequest },

            //NotFounds

            // Failed

        };

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/User") &&
                !context.Request.Path.StartsWithSegments("/api/Role"))
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
                    Status = false,
                    Msg = ex.Message
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
                    Msg = $"Đã xảy ra lỗi hệ thống, vui lòng thử lại sau."
                });
            }
        }
    }
}
