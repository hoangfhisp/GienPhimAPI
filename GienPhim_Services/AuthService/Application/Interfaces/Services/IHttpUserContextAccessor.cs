namespace GienPhim_Services.AuthService.Application.Interfaces.Services
{
    public interface IHttpUserContextAccessor
    {
        string? GetUserId(); // Lấy userId từ token
        string? GetEmail();
    }

}
