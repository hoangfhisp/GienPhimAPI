using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GienPhim_Services.InteractionService.Infrastructure.Services
{
    public class HttpContextUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUserService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public string UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? string.Empty;
        // Nếu token của bạn dùng claim khác (vd: "sub" hay "uid") thì đổi ClaimTypes.NameIdentifier cho đúng.
    }
}
