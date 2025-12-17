
using GienPhim_Services.WalletService.Domain.Exceptions;
using GienPhim_Services.WalletService.shared.Exceptions;
using GienPhim_Services.WalletService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.WalletService.Api.Middlewares
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
            // Invalid
            { typeof(InvalidBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InvalidIsActiveWalletException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserIdException), StatusCodes.Status400BadRequest },
            { typeof(InvalidWalletException), StatusCodes.Status400BadRequest },
            { typeof(InvalidCreateBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InvalidWalletTransactionException), StatusCodes.Status400BadRequest },
            { typeof(InvalidWalletTransactionBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUpdateBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InvalidWalletStatusException), StatusCodes.Status400BadRequest },
            //NotFounds

            // Failed

        };

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/Wallet"))
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
