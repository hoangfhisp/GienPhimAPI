namespace GienPhim_Services.WalletService.Application.Interfaces.Services
{
    public interface IHttpUserContextAccessor
    {
        string? GetUserId(); // Lấy userId từ token
        string? GetEmail();
    }

}
