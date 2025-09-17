using GienPhim_Services.AuthService.Application.Interfaces.Exceptions;
using GienPhim_Services.AuthService.shared.Exceptions;
using GienPhim_Services.AuthService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.AuthService.Api.Middlewares
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

        public async Task InvokeAsync(HttpContext context)
        {
            // Chỉ áp dụng middleware cho các endpoint bắt đầu bằng /api/Auth
            if (!context.Request.Path.StartsWithSegments("/api/Auth"))
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
                _logger.LogWarning(ex, "Domain error occurred.");
                var statusCode = (ex is IHasHttpStatus httpEx)
                    ? httpEx.StatusCode
                    : StatusCodes.Status400BadRequest;

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new { Status = false, Msg = ex.Message });
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
