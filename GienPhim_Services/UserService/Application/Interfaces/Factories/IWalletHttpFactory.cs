using GienPhim_Services.UserService.Application.DTOs.Http;

namespace GienPhim_Services.UserService.Application.Interfaces.Factories
{
    public interface IWalletHttpFactory
    {
        CreateWalletHttpRequestDTO toCreateWallet(string userId); // Tạo DTO để gọi API tạo ví
    }
}
